using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviesAPI.Entities;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController:ControllerBase
    {
        private readonly IRepository repository;
        public GenresController(IRepository repository) 
        {
        this.repository = repository;
        }

        [HttpGet]  //api/genre
        [HttpGet("list")] //api/genres/list
        [HttpGet("/allgenres")]  //allgenres
        public async Task<ActionResult<List<Genre>>> Get()
        {
            return await repository.GetAllGenres();
        }
        [HttpGet("{Id:int}/{param2=felipe}")]   //api/genres/example
        public ActionResult<Genre> Get(int Id, [FromServices] string param2)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = repository.GetGenreById(Id);

            if(genre == null)
            {
               return NotFound();
            }

            return genre;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
           repository.AddGenre(genre);
            return NoContent();
        }
        [HttpPut]
        public ActionResult Put([FromBody] Genre genre) 
        {
           
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}
