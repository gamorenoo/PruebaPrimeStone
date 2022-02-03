using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Common.Context
{
    class AppDbContextFactory : IDesignTimeDbContextFactory<AplicacionDBContext>
    {
        public AplicacionDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AplicacionDBContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;initial catalog=Estudiantes;");

            return new AplicacionDBContext(optionsBuilder.Options);
        }
    }
}
