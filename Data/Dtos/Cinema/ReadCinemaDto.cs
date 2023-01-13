using FilmesAPI.Models;

namespace FilmesAPI.Data.Dtos.Cinema
{
    public class ReadCinemaDto
    {
        public string Nome { get; set; }
        public int EnderecoId { get; set; }
        public Models.Endereco Endereco { get; set; }
        public Models.Gerente Gerente { get; set; }
    }
}