using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<LibroDTO>> Get()
        {
            var libros = await context.Libros.ToListAsync();
            var librosDTO = mapper.Map<IEnumerable<LibroDTO>>(libros);
            return librosDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerLibro")]
        public async Task<ActionResult<LibroConAutoresDTO>> Get(int id)
        {
            var libro = await context.Libros
                .Include(x => x.Autores)
                    .ThenInclude(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libro is null)
            {
                return NotFound();
            }

            var libroDTO = mapper.Map<LibroConAutoresDTO>(libro);

            return libroDTO;

        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreateDTO libroCreateDTO)
        {
            if(libroCreateDTO.AutoresIds is null || libroCreateDTO.AutoresIds.Count == 0)
            {
                ModelState.AddModelError(nameof(libroCreateDTO.AutoresIds), "No se puede crear un libro sin autores");
                return ValidationProblem();
            }

            var autoresIdsExisten = await context.Autores
                                        .Where(x => libroCreateDTO.AutoresIds.Contains(x.Id))
                                        .Select(x => x.Id).ToListAsync();

            if (autoresIdsExisten.Count != libroCreateDTO.AutoresIds.Count)
            {
                var autoresNoExisten = libroCreateDTO.AutoresIds.Except(autoresIdsExisten);
                var autoresNoExistenString = string.Join(",", autoresIdsExisten);
                var msjError = $"Los siguientes autores no existen: {autoresNoExistenString}";
                ModelState.AddModelError(nameof(LibroCreateDTO.AutoresIds), msjError);
                return ValidationProblem();
            }

            var libro = mapper.Map<Libro>(libroCreateDTO);
            AsignarOrdenAutores(libro);

            context.Add(libro);
            await context.SaveChangesAsync();

            var libroDTO = mapper.Map<LibroDTO>(libro);

            return CreatedAtRoute("ObtenerLibro", new { id = libro.Id }, libroDTO);
        }

        private void AsignarOrdenAutores(Libro libro)
        {
            if (libro.Autores is not null)
            {
                for(int i = 0; i < libro.Autores.Count; i++)
                {
                    libro.Autores[i].Orden = i;
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreateDTO libroCreateDTO)
        {
            if (libroCreateDTO.AutoresIds is null || libroCreateDTO.AutoresIds.Count == 0)
            {
                ModelState.AddModelError(nameof(libroCreateDTO.AutoresIds), "No se puede crear un libro sin autores");
                return ValidationProblem();
            }

            var autoresIdsExisten = await context.Autores
                                        .Where(x => libroCreateDTO.AutoresIds.Contains(x.Id))
                                        .Select(x => x.Id).ToListAsync();

            if (autoresIdsExisten.Count != libroCreateDTO.AutoresIds.Count)
            {
                var autoresNoExisten = libroCreateDTO.AutoresIds.Except(autoresIdsExisten);
                var autoresNoExistenString = string.Join(",", autoresIdsExisten);
                var msjError = $"Los siguientes autores no existen: {autoresNoExistenString}";
                ModelState.AddModelError(nameof(LibroCreateDTO.AutoresIds), msjError);
                return ValidationProblem();
            }


            var libroDB = await context.Libros
                .Include(x => x.Autores)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(libroDB is null)
            {
                return NotFound();
            }

            libroDB = mapper.Map(libroCreateDTO, libroDB);
            AsignarOrdenAutores(libroDB);

            await context.SaveChangesAsync();
            return Ok();
        }

        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var registrosBorrados = await context.Libros.Where(x => x.Id == id).ExecuteDeleteAsync();
        //    if(registrosBorrados == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok();
        //}
    }
}
