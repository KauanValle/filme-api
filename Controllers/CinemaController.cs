using System;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        public FilmeContext _context;
        public IMapper _mapper;

        public CinemaController (FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("adicionar")]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Add(cinema);
            _context.SaveChanges();

			// Passa o nome da action, valor da rota no caso {id}, vai retornar o filme no response
			return CreatedAtAction(nameof(ProcuraCinemaPorId), new { Id = cinema.Id }, cinema);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult AlteraCinema([FromBody] UpdateCinemaDto cinemaDto, int id)
        {
            Cinema? cinemaAntigo = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if(cinemaAntigo != null)
            {
                _mapper.Map(cinemaDto, cinemaAntigo);
                _context.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("todos")]
        public IActionResult ProcuraCinema([FromQuery] string? nomeDoFilme)
        {
            List<Cinema> cinemas = _context.Cinemas.ToList();
            if(cinemas == null)
            {
                return NotFound();
            }

            if(!string.IsNullOrEmpty(nomeDoFilme))
            {
                IEnumerable<Cinema> query = from cinema in cinemas
                            where cinema.Sessoes.Any(
                                sessao => sessao.Filme.Titulo == nomeDoFilme
                                ) select cinema;

                cinemas = query.ToList();
            }

            List<ReadCinemaDto> readDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);
            return Ok(readDto);
        }

        [HttpGet("get/{id}")]
        public IActionResult ProcuraCinemaPorId(int id)
        {
            Cinema? cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if(cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return Ok(cinemaDto);
            }
            return NotFound();
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Cinema? cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if(cinema != null)
            {
                _context.Remove(cinema);
                _context.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }
    }
}