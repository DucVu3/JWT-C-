using Api_Authentication.Entities;
using Api_Authentication.Handle.Email;
using Api_Authentication.Payloads.DTOs;
using Api_Authentication.Payloads.Requests;
using Api_Authentication.Payloads.Responses;

namespace Api_Authentication.Services.Interfaces
{
    public interface IUserService
    {
        string GenerateRefreshToken();
        TokenDTO GenerateAccessToken(User user);
        ResponseObject<TokenDTO> RenewAccessToken(TokenDTO request);
        Task<ResponseObject<TokenDTO>> Login(Request_Login request);
        Task<ResponseObject<UserDTO>> Register(Request_Register request);
        Task<IEnumerable<UserDTO>> GetAlls(int pageSize, int pageNumber);
        Task<ResponseObject<UserDTO>> ChangePassword(int userId, Request_ChangePassword request);
        string SendEmail(EmailTo emailTo);
        Task<string> ForgotPassword(Request_ForgotPassword request);
        Task<ResponseObject<UserDTO>> CreateNewPassword(ConfirmCreateNewPassword request);
        Task<ResponseObject<PostDTO>> CreateNewPost(int userId,Request_NewPost request);
        Task<ResponseObject<CommentDTO>> CreateNewComment(CommentDTO request);

    }
}
