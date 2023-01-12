using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly FilmeContext _context;
        private readonly IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("adiciona")]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Add(endereco);
            _context.SaveChanges();

            return CreatedAtAction(nameof(PegaEnderecoPorId), new {Id = endereco.Id}, endereco);
        }
        [HttpGet("get/{id}")]
        public IActionResult PegaEnderecoPorId(int id)
        {
            Endereco? endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if(endereco != null)
            {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
                return Ok(enderecoDto);
            }
            return NotFound();
        }
        [HttpGet("todos")]
        public IActionResult PegaTodosEnderecos()
        {
            return Ok(_context.Enderecos);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizaEndereco([FromBody] UpdateEnderecoDto enderecoDto, int id)
        {
            Endereco? enderecoAntigo = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if(enderecoAntigo != null)
            {
                _mapper.Map(enderecoDto, enderecoAntigo);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Endereco? endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if(endereco != null)
            {
                _context.Remove(endereco);
                _context.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }
    }
}


