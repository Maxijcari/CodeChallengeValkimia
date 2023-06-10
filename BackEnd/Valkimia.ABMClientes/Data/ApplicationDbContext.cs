using Microsoft.EntityFrameworkCore;

namespace Valkimia.ABMClientes.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }
    }
}
