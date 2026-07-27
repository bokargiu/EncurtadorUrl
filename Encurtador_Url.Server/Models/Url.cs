namespace Encurtador_Url.Server.Models
{
    public class Url
    {
        public Guid Id = new Guid();
        public string OriginalUrl { get; set; } = string.Empty;
        public string UrlCode { get; set; } = string.Empty;
    }
}
