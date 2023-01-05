using FilmesAPI.Repositorys;

namespace FilmesAPI.Services
{
    public interface IFilmeService 
    {
        void GetFilmes();
    }

    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeService (IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public void GetFilmes()
        {
            var teste = true;
            _filmeRepository.GetFilmes();
        }
    }
}


