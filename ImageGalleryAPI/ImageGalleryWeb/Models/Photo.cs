using System.Text.Json;

namespace ImageGalleryWeb.Models
{
    public class Photo
    {

        
        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] UploadedImage { get; set; }

        public DateTime UploadDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public decimal Size { get; set; }
    }   
}
