using AutoMapper;
using SistemaMatriculas.Domain.Cursos;
using SistemaMatriculas.Infraestructure.Repository;

namespace SistemaMatriculas.Application.Services
{
    public class CursoService : IService<CursoDto>
    {
        private IRepository<Curso> _repository;
        private IMapper _mapper;
        public List<string> Errors { get; }
        public CursoService(IRepository<Curso> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<CursoDto>> Get()
        {
            var cursos = await _repository.Get();

            return cursos.Select(c => _mapper.Map<CursoDto>(c));
        }
        public async Task<CursoDto> GetById(int id)
        {
            var curso = await _repository.GetById(id);

            if (curso != null)
            {
                var cursoDto = _mapper.Map<CursoDto>(curso);
                return cursoDto;
            }

            return null;
        }

        public async Task<CursoDto> Delete(int id)
        {
            var curso = await _repository.GetById(id);

            if (curso != null)
            {
                var cursoDto = _mapper.Map<CursoDto>(curso);

                _repository.Delete(curso);
                await _repository.Save();

                return cursoDto;
            }

            return null;
        }

        public async Task<CursoDto> Add(CursoDto dto)
        {
            var curso = _mapper.Map<Curso>(dto);

            await _repository.Add(curso);
            await _repository.Save();

            var cursoDto = _mapper.Map<CursoDto>(curso);

            return cursoDto;
        }

        public async Task<CursoDto> Update(int id, CursoDto dto)
        {
            var curso = await _repository.GetById(id);

            if (curso != null) 
            {
                curso = _mapper.Map<CursoDto, Curso>(dto, curso);

                _repository.Update(curso);
                await _repository.Save();

                var cursoDto = _mapper.Map<CursoDto>(curso);

                return cursoDto;
            }

            return null;
        }

        public bool Validate(CursoDto dto, bool isUpdate)
        {
            

            if (isUpdate)
            {
                if (_repository.Search(c => c.Nombre == dto.Nombre && dto.Id != c.Id).Count() > 0)
                {
                    Errors.Add("No se puede modificar un curso con el nombre de otro existente.");
                    return false;
                }
            }
            else
            {
                if (_repository.Search(c => c.Nombre == dto.Nombre).Count() > 0)
                {
                    Errors.Add("No se puede agregar un curso con el nombre de otro existente.");
                    return false;
                }
            }

            return true;
        }
    }
}
