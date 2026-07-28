using Encurtador_Url.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;
using System.Text;

namespace Encurtador_Url.Server.Services.UrlServices
{
    public class UrlService : IUrlService
    {
        private DataBase _context;
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
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<string> CheckCod(string cod)
        {
            var index = 9;
            while (await ExistsUrl(cod))
            {
                List<Url> urls = await _context.Urls.Where(u => u.UrlCode.StartsWith(cod.Substring(0, index))).ToListAsync();
                if (urls.Count < 62)
                {
                    char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToArray();
                    foreach (var item in urls)
                    {
                        chars.Where(c => c != item.UrlCode[index]).ToArray();
                    }
                    List<char> newCode = cod.ToArray().ToList();
                    newCode.Remove(newCode[index]);
                    newCode.Add(chars[Random.Shared.Next(0, chars.Length)]);
                    break;
                }
                index--;
                if (index == -1)
                    cod = GenerateRandomCode(10);
            }
            return cod;
        }
        private async Task<bool> ExistsUrl(string url)
        {
            return await _context.Urls.AnyAsync(u => u.OriginalUrl == url);
        }
    }
}
