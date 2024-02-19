using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace Api_Authentication.Handle.HandleImage
{
    public class UploadImage
    {
        static string cloudName = "ddvdbxpdy";
        static string apiKey = "751325563241239";
        static string apiSecret = "ZNX3ume6gF6XKG_W2hytwJDdqU4";
        static public Account account = new Account(cloudName, apiKey, apiSecret);
        static public Cloudinary _cloudinary = new Cloudinary(account);
        public static async Task<string> Upfile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Không có tập tin được chọn.");
            }
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = "xyz-abc" + "_" + DateTime.Now.Ticks + "image",
                    Transformation = new Transformation().Width(300).Height(400).Crop("fill")
                };
                var uploadResult = await UploadImage._cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }
                string imageUrl = uploadResult.SecureUrl.ToString();
                return imageUrl;
            }
        }
    }
}
