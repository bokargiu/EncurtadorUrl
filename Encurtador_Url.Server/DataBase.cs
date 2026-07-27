using Microsoft.EntityFrameworkCore;

namespace Encurtador_Url.Server
{
    public class DataBase : DbContext
    {
        public DataBase(DbContextOptions<DataBase> options) : base(options) {}
    }
}
