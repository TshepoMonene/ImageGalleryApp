using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImageGalleryWeb.Models
{
    public class Photo
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("uploadedImage")]
        public byte[]? UploadedImage { get; set; }

        public DateTime UploadDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public decimal Size { get; set; }
    }   
}
