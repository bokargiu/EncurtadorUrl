using Encurtador_Url.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;
using System.Text;

namespace Encurtador_Url.Server.Services.UrlServices
{
    public class UrlService : IUrlService
    {
        private readonly DataBase _context;
        public UrlService(DataBase context)
        {
            _context = context;
        }
        public async Task<Url?> get(string code)
        {
            return await _context.Urls.FirstOrDefaultAsync(u => u.UrlCode == code);
        }
        public async Task<Url> shorten(string url)
        {
            if (await ExistsUrl(url) is Url u)
                    return u;

            string cod = GenerateRandomCode(10);

            cod = await CheckCod(cod);

            Url newUrl = new Url()
            {
                OriginalUrl = url,
                UrlCode = cod
            };
            _context.Urls.Add(newUrl);
            await _context.SaveChangesAsync();

            return newUrl;
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
        }

        private async Task<string> CheckCod(string cod)
        {
            var index = cod.Length - 1;
            while (await ExistsCode(cod))
            {
                List<string> urls = await _context.Urls.AsNoTracking()
                                                    .Where(u => u.UrlCode
                                                    .StartsWith(cod.Substring(0, index)))
                                                    .Select(u => u.UrlCode)
                                                    .ToListAsync();
                if (urls.Count < (int)Math.Pow(62, cod.Length - index))
                {
                    char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToArray();
                    foreach (var item in urls)
                    {
                        chars = chars.Where(c => c != item[index]).ToArray();
                    }
                    if (chars.Length != 0)
                    {
                        char[] newCode = cod.ToArray();
                        newCode[index] = chars[Random.Shared.Next(0, chars.Length)];
                        cod = new string(newCode);
                        break;
                    }
                }
                index--;
                if (index >= 8)
                {
                    index = cod.Length - 1;
                    cod = GenerateRandomCode(10);
                }
            }
            return cod;
        }
        private async Task<Url?> ExistsUrl(string url)
        {
            return await _context.Urls.FirstOrDefaultAsync(u => u.OriginalUrl == url);
        }
        private async Task<bool> ExistsCode(string code)
        {
            return await _context.Urls.AsNoTracking().AnyAsync(u => u.UrlCode == code);
        }
    }
}
