using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private readonly FilmeContext _context;
        private readonly IMapper _mapper;

        public GerenteController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("adicionar")]
        public IActionResult AdicionarGerente([FromBody] CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerente.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ProcuraGerentePorId), new { Id = gerente.Id}, gerente);
        }

        [HttpGet("get/{id}")]
        public IActionResult ProcuraGerentePorId(int id)
        {
            Gerente? gerente = _context.Gerente.FirstOrDefault(gerente => gerente.Id == id);
            if(gerente != null)
            {
                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

                return Ok(gerenteDto);
            }
            return NotFound();
        }
    }
}