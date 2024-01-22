using static System.Net.Mime.MediaTypeNames;

namespace ImageGalleryWeb.Extensions
{
    public static class ExtensionMethods
    {

        public static byte[] ConvertToBytes(this IFormFile file)
        {
            long length = file.Length;
            
            using var filestream = file.OpenReadStream();
            byte[] bytes = new byte[length];
            filestream.Read(bytes, 0, (int)file.Length);

            return bytes;
        }
        public static string GetImage(this byte[] byteArray)
        {
            string imreBase64Data = Convert.ToBase64String(byteArray);
            string imgDataURL = string.Format("data:image/jpg;base64,{0}", imreBase64Data);  
            return imgDataURL;
        }
    }
}
