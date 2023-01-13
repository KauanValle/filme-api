using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
	public class Filme
	{
		[Key]
		[Required]
		public int id { get; set; }
		[Required(ErrorMessage = "O campo título é obrigatório")]
		public string Titulo { get; set; }
		public string Diretor { get; set; }
		public string Genero { get; set; }
		public int Duracao { get; set; }

		[JsonIgnore]
		public virtual List<Sessao> Sessoes { get; set; }
	}
}

