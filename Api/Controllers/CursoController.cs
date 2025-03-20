using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaMatriculas.Application.Services;
using SistemaMatriculas.Domain.Cursos;

namespace SistemaMatriculas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private IService<CursoDto> _service;
        private IValidator<CursoDto> _validator;

        public CursoController([FromKeyedServices("cursoService")] IService<CursoDto> service,
            IValidator<CursoDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IEnumerable<CursoDto>> Get()
            => await _service.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> GetById(int id)
        {
            var cursoDto = await _service.GetById(id);

            return (cursoDto == null) ? NotFound() : Ok(cursoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CursoDto>> Delete(int id)
        {
            var cursoDto = await _service.Delete(id);

            return (cursoDto == null) ? NotFound() : Ok(cursoDto);
        }

        [HttpPost]
        public async Task<ActionResult<CursoDto>> Add(CursoDto dto)
        {
            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            if(!_service.Validate(dto, false))
            {
                return BadRequest(_service.Errors);
            }

            var cursoDto = await _service.Add(dto);

            return CreatedAtAction(nameof(GetById), new { id = cursoDto.Id}, cursoDto);
        }

        [HttpPut]
        public async Task<ActionResult<CursoDto>> Update(int id, CursoDto dto)
        {
            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            if (!_service.Validate(dto, true))
            {
                return BadRequest(_service.Errors);
            }
            
            var cursoDto = await _service.Update(id, dto);

            return (cursoDto == null) ? NotFound() : Ok(cursoDto);
        }

    }
}
