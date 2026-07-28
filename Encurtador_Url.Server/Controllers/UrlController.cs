using Encurtador_Url.Server.DTOs;
using Encurtador_Url.Server.Services.UrlServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Encurtador_Url.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private IUrlService _urlService;
        
        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost]
        public IActionResult shorten(ToShorten dto)
        {
            var url = _urlService.shorten(dto.Url);
            return Ok(new
            {
                url = $"{dto.domain}/red/{url.Result.UrlCode}"
            });
        }

        [HttpGet("{code}")]
        public IActionResult getShortenedUrl(string code)
        {
            var url = _urlService.get(code);
            if (url.Result == null)
                return NotFound();

            return Ok(new
            {
                url = url.Result.OriginalUrl
            });
        }
    }
}
