using System;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FilmeController : ControllerBase
	{
        private readonly FilmeContext _context;
		private readonly IMapper _mapper;

		private readonly IFilmeService _filmeService;

		public FilmeController(FilmeContext filmeContext, IMapper mapper)
		{
			_context = filmeContext;
			_mapper = mapper;
		}

		// Injeção de dependencia;

		// public FilmeController(IFilmeService filmeService)
		// {
		// 	_filmeService = filmeService;
		// }

		[HttpPost]
		public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
		{
			Filme filme = _mapper.Map<Filme>(filmeDto);

			_context.Filmes.Add(filme);
			_context.SaveChanges();

			// Passa o nome da action, valor da rota no caso {id}, vai retornar o filme no response
			return CreatedAtAction(nameof(RecuperaFilmePorId), new { Id = filme.id }, filme);
		}

		[HttpGet("todos")]
		public IActionResult RecuperaFilmes()
		{
			// _filmeService.GetFilmes();
			return Ok(_context.Filmes);
		}

		[HttpGet("get/{id}")]
		public IActionResult RecuperaFilmePorId(int id)
		{
			Filme? filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);
			if(filme != null)
			{
				ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

				return Ok(filmeDto);
			}

			return NotFound();
		}

		[HttpPut("atualizar/{id}")]
		public IActionResult AtualizaFilmePorId(int id,[FromBody] UpdateFilmeDto filmeDto)
		{
			Filme? filmeAntigo = _context.Filmes.FirstOrDefault(filme => filme.id == id);

			if(filmeAntigo != null)
			{
				_mapper.Map(filmeDto, filmeAntigo);
				_context.SaveChanges();

				return NoContent();
			}

			return NotFound();
		}

		[HttpDelete("deletar/{id}")]
		public IActionResult DeletaFilme(int id)
		{
			Filme? filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);
			if(filme != null)
			{
				_context.Remove(filme);
				_context.SaveChanges();

				return NoContent();
			}
			return NotFound();
		}
	}
}