using Microsoft.EntityFrameworkCore;
using TheLionsDen.Services.Database;

namespace TheLionsDen.DbSetup
{
    public class DbHelper
    {
        public void Init(TheLionsDenContext context)
        {
            context.Database.Migrate();
        }

        public void InsertData(TheLionsDenContext context)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "DbSetup", "database.sql");
            var query = File.ReadAllText(path);
            context.Database.ExecuteSqlRaw(query);
        }
    }
}
