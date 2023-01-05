namespace FilmesAPI.Repositorys
{
    public interface IFilmeRepository
    {
        void GetFilmes();
    }

    public class FilmeRepository : IFilmeRepository
    {
        public void GetFilmes()
        {
            var teste = true;
        }
    }
}


