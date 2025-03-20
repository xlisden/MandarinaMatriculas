using Microsoft.EntityFrameworkCore;
using SistemaMatriculas.Domain.Cursos;

namespace SistemaMatriculas.Infraestructure.Repository
{
    public class CursoRepository : IRepository<Curso>
    {
        private ApplicationDbContext _context;

        public CursoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Curso>> Get()
            => await _context.Cursos.ToListAsync();

        public async Task<Curso> GetById(int id)
            => await _context.Cursos.FindAsync(id)!;

        public void Delete(Curso entity)
            => _context.Cursos.Remove(entity);

        public async Task Add(Curso entity)
            => await _context.Cursos.AddAsync(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public void Update(Curso entity)
        {
            _context.Cursos.Attach(entity);
            _context.Cursos.Entry(entity);
        }

        public IEnumerable<Curso> Search(Func<Curso, bool> filter)
            => _context.Cursos.Where(filter).ToList();

    }
}
