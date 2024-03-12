using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviesAPI.Entities;
using MoviesAPI.Filters;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController:ControllerBase
    {
        private readonly ILogger<GenresController> logger;
        public GenresController(ILogger<GenresController> logger) 
        {
            this.logger = logger;
        }

        [HttpGet]  //api/genre
        
        public async Task<ActionResult<List<Genre>>> Get()
        {
            logger.LogInformation("Getting all the genres");
            return new List<Genre>() { new Genre() { Id = 1, Name="Comedy" } };
        }
        [HttpGet("{Id:int}", Name ="getGenre")]   //api/genres/example
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Genre> Get(int Id, string param2)
        {

            logger.LogDebug("get by Id method executing...");
            var genre = repository.GetGenreById(Id);

            if (genre == null)
            {
                logger.LogWarning($"Genre with Id {Id} not found");
                logger.LogError("this is an error");
                //throw new ApplicationException();
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
