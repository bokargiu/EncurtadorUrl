namespace Encurtador_Url.Server.DTOs
{
    public class ToShorten
    {
        public string Url { get; set; }
        public string domain { get; set; }
    }

    public class ShortenedUrl
    {
        public string Url { get; set; }
    }
}
