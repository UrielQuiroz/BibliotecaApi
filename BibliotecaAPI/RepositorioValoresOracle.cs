using BibliotecaAPI.Entidades;

namespace BibliotecaAPI
{
    public class RepositorioValoresOracle : IRepositorioValores
    {
        private List<Valor> _valores;

        public RepositorioValoresOracle()
        {
            _valores = new List<Valor>
            {
                new Valor { Id = 3, Nombre = "Valor 3"},
                new Valor { Id = 4, Nombre = "Valor 4"},
                new Valor { Id = 5, Nombre = "Valor 5"}
            };
        }
        public IEnumerable<Valor> ObtenerValores()
        {
            return _valores;
        }

        public void InsertarValor(Valor valor) 
        {
            _valores.Add(valor);
        }
    }
}
