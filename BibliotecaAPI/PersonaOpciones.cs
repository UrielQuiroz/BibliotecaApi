using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI
{
    public class PersonaOpciones
    {
        public const string Seccion = "seccion_1";
        [Required]
        public required string Jugador { get; set; }
        [Required]
        public int Edad { get; set; }
    }
}
