using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos.Gerente
{
    public class CreateGerenteDto
    {
        public string Nome { get; set; }
    }

    public class ReadGerenteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}