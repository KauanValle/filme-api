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
		}

		public DbSet<Filme> Filmes { get; set; }
		public DbSet<Cinema> Cinemas { get; set; }
		public DbSet<Endereco> Enderecos { get; set; }
		public DbSet<Gerente> Gerentes {get; set;}
	}
}
