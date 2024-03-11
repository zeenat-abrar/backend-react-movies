﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<GenresController> logger;
        public GenresController(IRepository repository, ILogger<GenresController> logger) 
        {
        this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]  //api/genre
        [HttpGet("list")] //api/genres/list
        [HttpGet("/allgenres")]  //allgenres
        public async Task<ActionResult<List<Genre>>> Get()
        {
            logger.LogInformation("Getting all the genres");
            return await repository.GetAllGenres();
        }
        [HttpGet("{Id:int}", Name ="getGenre")]   //api/genres/example
        public ActionResult<Genre> Get(int Id, string param2)
        {

            logger.LogDebug("get by Id method executing...");
            var genre = repository.GetGenreById(Id);

            if (genre == null)
            {
                logger.LogWarning($"Genre with Id {Id} not found");
                logger.LogError("this is an error");
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
