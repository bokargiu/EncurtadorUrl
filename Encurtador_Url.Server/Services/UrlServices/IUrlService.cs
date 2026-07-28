using Encurtador_Url.Server.Models;

namespace Encurtador_Url.Server.Services.UrlServices
{
    public interface IUrlService
    {
        public Task<Url> shorten(string url);
        public Task<Url?> get(string code);
    }
}
