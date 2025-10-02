using BibliotecaAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Autor 
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "El campo Nombre es requerido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }

        public List<Libro> Libros { get; set; } = new List<Libro>();

    }
}
