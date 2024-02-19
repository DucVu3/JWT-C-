using Api_Authentication.DataContext;
using Api_Authentication.Entities;
using Api_Authentication.Handle.HandleImage;
using Api_Authentication.Payloads.Converters;
using Api_Authentication.Payloads.DTOs;
using Api_Authentication.Payloads.Requests;
using Api_Authentication.Payloads.Responses;
using Api_Authentication.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Mail;
using System.Security.Cryptography;
using BcryptNet = BCrypt.Net.BCrypt;
using SmtpClient = System.Net.Mail.SmtpClient;
using AutoMapper;
using Api_Authentication.Handle.Email;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

namespace Api_Authentication.Services.Implements
{
    public class UserService : BaseService,IUserService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ResponseObject<UserDTO> _responseObject;
        private readonly ResponseObject<TokenDTO> _tokenResponse;
        private readonly UserConverter _converter;
        public UserService(IMapper mapper, IConfiguration configuration, ResponseObject<UserDTO> responseObject, ResponseObject<TokenDTO> reponseObjectToken, UserConverter converter)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _responseObject = responseObject;
            _tokenResponse = reponseObjectToken;
            _converter = converter;
            _configuration = configuration;


        }
        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var item = RandomNumberGenerator.Create())
            {
                item.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        private int GenerateCodeActive()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
        public TokenDTO GenerateAccessToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value!);

            var decentralization = _context.roles.FirstOrDefault(x => x.Id == user.RoleId);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Username", user.UserName),
                    new Claim("Avatar", user.Avatar),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim(ClaimTypes.Role, decentralization?.RoleName ?? "")
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                ExpiredTime = DateTime.UtcNow.AddHours(4),
                UserId = user.Id
            };

            _context.refreshTokens.Add(rf);
            _context.SaveChanges();

            TokenDTO tokenDTO = new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return tokenDTO;


        }

        public async Task<ResponseObject<UserDTO>> Register(Request_Register request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.FullName) || string.IsNullOrWhiteSpace(request.Email))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui long dien day du thong tin", null);
            }
            if (_context.users.Any(x => x.Email.Equals(request.Email)))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email da ton tai", null);
            }
            if (_context.users.Any(x => x.UserName.Equals(request.UserName)))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "UserName da ton tai", null);
            }
            User user = new User();
            user.UserName = request.UserName;
            user.Password = BcryptNet.HashPassword(request.Password);
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.DateOfBirth = request.DateOfBirth;
            user.RoleId = 3;
            user.Avatar = await UploadImage.Upfile(request.Avatar);
            _context.users.Add(user);
            _context.SaveChanges();
            return _responseObject.ResponseSuccess("Dang ky thanh cong", _converter.EntityToDTO(user));
        }

        public ResponseObject<TokenDTO> RenewAccessToken(TokenDTO request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);

            var tokenValidation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value))
            };

            try
            {
                var tokenAuthentication = jwtTokenHandler.ValidateToken(request.AccessToken, tokenValidation, out var validatedToken);
                if (validatedToken is not JwtSecurityToken jwtSecurityToken || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return _tokenResponse.ResponseError(StatusCodes.Status400BadRequest, "Token không hợp lệ", null);
                }
                RefreshToken refreshToken = _context.refreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken);
                if (refreshToken == null)
                {
                    return _tokenResponse.ResponseError(StatusCodes.Status404NotFound, "RefreshToken không tồn tại trong database", null);
                }
                if (refreshToken.ExpiredTime < DateTime.Now)
                {
                    return _tokenResponse.ResponseError(StatusCodes.Status401Unauthorized, "Token chưa hết hạn", null);
                }
                var user = _context.users.FirstOrDefault(x => x.Id == refreshToken.UserId);
                if (user == null)
                {
                    return _tokenResponse.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
                }
                var newToken = GenerateAccessToken(user);

                return _tokenResponse.ResponseSuccess("Làm mới token thành công", newToken);
            }
            catch (Exception ex)
            {
                return _tokenResponse.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
            }
        }

        public async Task<ResponseObject<TokenDTO>> Login(Request_Login request)
        {
            if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.UserName))
            {
                return _tokenResponse.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin", null);
            }

            var user = await _context.users.FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName));
            if (user is null)
            {
                return _tokenResponse.ResponseError(StatusCodes.Status404NotFound, "Tên tài khoản không tồn tại", null);
            }

            bool isPasswordValid = BcryptNet.Verify(request.Password, user.Password);
            if (!isPasswordValid)
            {
                return _tokenResponse.ResponseError(StatusCodes.Status400BadRequest, "Tên đăng nhập hoặc mật khẩu không chính xác", null);
            }
            else
            {
                return _tokenResponse.ResponseSuccess("Đăng nhập thành công", GenerateAccessToken(user));
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAlls(int pageSize, int pageNumber)
        {
            var list = await _context.users.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => _converter.EntityToDTO(x)).ToListAsync();
            return list;
        }

        public async Task<ResponseObject<UserDTO>> ChangePassword(int userId, Request_ChangePassword request)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.Id == userId);
            if (!BcryptNet.Verify(request.OldPassword, user.Password))
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Mật khẩu cũ không chính xác", null);
            }
            user.Password = BcryptNet.HashPassword(request.NewPassword);
            _context.users.Update(user);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Đổi mật khẩu thành công", _converter.EntityToDTO(user));
        }

        public string SendEmail(EmailTo emailTo)
        {
            if (!Validate.IsValidEmail(emailTo.Mail))
            {
                return "Định dạng không hợp lệ";
            }
            var smtpClient = new SmtpClient("smt.gmail.com")
            {
                Port = 456,
                Credentials = new NetworkCredential("vungocduc78@gmail.com","asdajsdasjdajs"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("vungocduc78@gmail.com");
                message.To.Add(emailTo.Mail);
                message.Subject = emailTo.Subject;
                message.Body = emailTo.Content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);

                return "Gửi email thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }

        }

        public async Task<string> ForgotPassword(Request_ForgotPassword request)
        {
            User user = await _context.users.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            if (user is null)
            {
                return "Email không tồn tại trong hệ thống";
            }
            else
            {
                var confirms = _context.confirmEmails.Where(x => x.UserId == user.Id).ToList();
                _context.confirmEmails.RemoveRange(confirms);
                await _context.SaveChangesAsync();
                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    UserId = user.Id,
                    IsConfirm = false,
                    ExpiredTime = DateTime.Now.AddHours(4),
                    CodeActive = "MyBugs" + GenerateCodeActive().ToString()
                };
                await _context.confirmEmails.AddAsync(confirmEmail);
                await _context.SaveChangesAsync();
                string message = SendEmail(new EmailTo
                {
                    Mail = request.Email,
                    Subject = "Nhận mã xác nhận để tạo mật khẩu mới từ đây: ",
                    Content = $"Mã kích hoạt của bạn là: {confirmEmail.CodeActive}, mã này sẽ hết hạn sau 4 tiếng"
                });
                return "Gửi mã xác nhận về email thành công, vui lòng kiểm tra email";
            }
        }
        public async Task<ResponseObject<UserDTO>> CreateNewPassword(ConfirmCreateNewPassword request)
        {
            ConfirmEmail confirmEmail = await _context.confirmEmails.Where(x => x.CodeActive.Equals(request.CodeActive)).FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận không chính xác", null);
            }
            if (confirmEmail.ExpiredTime < DateTime.Now)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận đã hết hạn", null);
            }
            User user = _context.users.FirstOrDefault(x => x.Id == confirmEmail.UserId);
            user.Password = BcryptNet.HashPassword(request.NewPassword);
            _context.confirmEmails.Remove(confirmEmail);
            _context.users.Update(user);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Tạo mật khẩu mới thành công", _converter.EntityToDTO(user));

        }


        public Task<ResponseObject<CommentDTO>> CreateNewComment(CommentDTO request)
        {
            throw new NotImplementedException();
        }
    }
    

}
