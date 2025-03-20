using FluentValidation;
using SistemaMatriculas.Domain.Cursos;
using System.Data;

namespace SistemaMatriculas.Application.Validators
{
    public class CursoValidation: AbstractValidator<CursoDto>
    {
        public CursoValidation()
        {
            RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre del  curso no puede estar vacio");
        }
    }
}
