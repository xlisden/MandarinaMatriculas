using AutoMapper;
using SistemaMatriculas.Domain.Cursos;

namespace SistemaMatriculas.Application.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Curso
            CreateMap<Curso, CursoDto>();
            CreateMap<CursoDto, Curso>();

        }
    }
}
