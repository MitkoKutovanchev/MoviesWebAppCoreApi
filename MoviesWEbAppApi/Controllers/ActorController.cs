using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DB.Repositories;
using Data.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using MoviesWEbAppApi.BindModels;
using MoviesWEbAppApi.wwwroot.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesWEbAppApi.Controllers
{
    [Route("api/[controller]")]
    public class ActorController : Controller
    {

        ActorRepository actorRepo = new ActorRepository();
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            List<ActorBindModel> actorsModel = new List<ActorBindModel>();

            foreach (var actor in actorRepo.GetAll())
            {
                actorsModel.Add(new ActorBindModel
                {
                    FirstName = actor.FirstName,
                    LastName = actor.LastName
                });
            }
            return Ok(actorsModel);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (actorRepo.Get(id) == null)
            {
                return NotFound();
            }
            return Ok(actorRepo.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]ActorBindModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }

            Actor actor = new Actor();
            actor.FirstName = model.FirstName;
            actor.LastName = model.LastName;

            actorRepo.Insert(actor);

            return Ok(actor);

        }
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ActorBindModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }

            if (actorRepo.Get(id) == null)
            {
                return NotFound();
            }

            Actor actor = actorRepo.Get(id);
            actor.FirstName = model.FirstName;
            actor.LastName = model.LastName;

            actorRepo.Update(actor);

            return Ok(actor);

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }
            Actor actor = actorRepo.Get(id);

            if (actor == null)
            {
                return NotFound();
            }

            actorRepo.Delete(actor);

            return Ok(actor);
        }
    }
}
