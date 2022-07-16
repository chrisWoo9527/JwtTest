using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Sql.Data
{
    internal class MirDesignTimeDbContext : IDesignTimeDbContextFactory<MirDbContext>
    {
        public MirDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MirDbContext> builder = new();
            builder.UseSqlServer("Server=ChrisServer;Initial Catalog=IdentityDb;Persist Security Info=False;User ID=sa;Password=1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=300;");
            return new MirDbContext(builder.Options);
        }
    }
}
