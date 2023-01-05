using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos
{
    public class ReadFilmeDto
    {
        [Key]
		[Required]
		public int id { get; set; }
		[Required(ErrorMessage = "O campo título é obrigatório")]
		public string Titulo { get; set; }
		public string Diretor { get; set; }
		public string Genero { get; set; }
		public int Duracao { get; set; }
        public DateTime HoraConsulta { get; set; }
    }
}