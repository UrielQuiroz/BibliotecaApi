using Microsoft.Extensions.Options;

namespace BibliotecaAPI
{
    public class PagosProcesamiento
    {
        private TarifaOpciones _tarifaOpciones;

        public PagosProcesamiento(IOptionsMonitor<TarifaOpciones> optionsMonitor)
        {
            _tarifaOpciones = optionsMonitor.CurrentValue;

            optionsMonitor.OnChange(nuevaTrifa =>
            {
                Console.WriteLine("Tarifa acualizada");
                _tarifaOpciones = nuevaTrifa;
            });
        }

        public void ProcesarPago()
        {
            //Aqui usamos las tarifas
        }

        public TarifaOpciones ObtenerTarifas()
        {
            return _tarifaOpciones;
        }
    }
}
