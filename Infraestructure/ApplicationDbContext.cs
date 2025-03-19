using Microsoft.EntityFrameworkCore;
using SistemaMatriculas.Domain.Cursos;

namespace SistemaMatriculas.Infraestructure
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options 
        ) : base( options ) 
        {
        }

        public DbSet<Curso> Cursos { get; set; }
     
    }
}
