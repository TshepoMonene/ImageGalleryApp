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
    }
}
