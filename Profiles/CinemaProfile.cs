using AutoMapper;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            // Um request de DTO para uma model Cinema.
            CreateMap<CreateCinemaDto, Cinema>();
            // Uma model cinema para um CinemaDTO.
            CreateMap<Cinema, ReadCinemaDto>();
            // Uma model de atualizacao para uma model de cinema.
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}