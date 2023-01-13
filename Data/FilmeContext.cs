using System;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
	public class FilmeContext : DbContext
	{
		public FilmeContext(DbContextOptions<FilmeContext> opt) : base (opt)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Endereco>() // Entidade do tipo endereco
				.HasOne(endereco => endereco.Cinema) // Tem um cinema
				.WithOne(cinema => cinema.Endereco) // Logo o cinema possui um endereco
				.HasForeignKey<Cinema>(cinema => cinema.EnderecoId); // Chave Estrangeira está em Cinema e é o EnderecoId

			builder.Entity<Cinema>()
				.HasOne(cinema => cinema.Gerente)
				.WithMany(gerente => gerente.Cinemas)
				.HasForeignKey(cinema => cinema.GerenteId);

			builder.Entity<Sessao>()
				.HasOne(sessao => sessao.Filme)
				.WithMany(filme => filme.Sessoes)
				.HasForeignKey(sessao => sessao.FilmeId);

			builder.Entity<Sessao>()
				.HasOne(sessao => sessao.Cinema)
				.WithMany(cinema => cinema.Sessoes)
				.HasForeignKey(sessao => sessao.CinemaId);
		}

		public DbSet<Filme> Filmes { get; set; }
		public DbSet<Cinema> Cinemas { get; set; }
		public DbSet<Endereco> Enderecos { get; set; }
		public DbSet<Gerente> Gerente {get; set;}
		public DbSet<Sessao> Sessoes { get; set; }
	}
}
