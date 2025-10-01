using BibliotecaAPI.Entidades;

namespace BibliotecaAPI
{
    public interface IRepositorioValores
    {
        public IEnumerable<Valor> ObtenerValores();
    }
}
