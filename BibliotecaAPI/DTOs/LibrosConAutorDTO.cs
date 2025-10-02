namespace BibliotecaAPI.DTOs
{
    public class LibrosConAutorDTO : LibroDTO
    {
        public int AutorId { get; set; }
        public required string AutorNombre { get; set; }
    }
}
