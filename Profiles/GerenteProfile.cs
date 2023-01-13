using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            CreateMap<Gerente, ReadGerenteDto>() // No readGerenteDto
                .ForMember(gerente => gerente.Cinemas, opts => opts // Para o objeto Cinemas
                .MapFrom(gerente => gerente.Cinemas.Select // Quero que ele selecione apenas
                (c => new { c.Id, c.Nome, c.Endereco, c.EnderecoId }))); // O campo ID / NOME / ENDERECO / ENDERECO.ID
        }
    }
}