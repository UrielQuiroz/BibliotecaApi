using BibliotecaAPI.Entidades;

namespace BibliotecaAPI
{
    public class RepositorioValoresOracle : IRepositorioValores
    {
        public IEnumerable<Valor> ObtenerValores()
        {
            return new List<Valor>
            {
                new Valor { Id = 3, Nombre = "Valor 3"},
                new Valor { Id = 4, Nombre = "Valor 4"},
                new Valor { Id = 5, Nombre = "Valor 5"}
            };
        }
    }
}
