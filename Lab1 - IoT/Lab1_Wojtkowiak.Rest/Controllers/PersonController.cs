using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using FirstApp.models;
using System.Linq;

namespace Lab1_Wojtkowiak.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class peopleController : ControllerBase
    {

        private readonly PeopleDb db;
        public peopleController(PeopleDb db)
        {
            this.db = db;
        }

        //get all results
        [HttpGet]
        public IActionResult Get()
        {
            var people = db.People.ToList();
            return Ok(people);
        }

        //create new record POST
        [HttpPost]
        public class CreatePerson
        {
            public string PersonDescription { get; set; }
        }

        [FunctionName("CreatePerson")]
        public static async Task<IActionResult>CreatePerson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "people")]HttpRequest req, TraceWriter log)
        {
            log.Info("Creating a new person");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CreatePerson>(requestBody);

            var person = new Person() { PersonDescription = input.PersonDescription };
            items.Add(person);
            return new OkObjectResult(person);
        }


        //update existing record PUT
        [HttpPut]
        [FunctionName("UpdatePerson")]
        public static async Task<IActionResult> UpdatePerson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "people/{id}")]HttpRequest req, 
            TraceWriter log, string id)
        {
            var person = items.FirstOrDefault(t => t.Id == id);
            if (person == null)
            {
                return new NotFoundResult();
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<UpdatePerson>(requestBody);

            person.IsCompleted = updated.IsCompleted;
            if (!string.IsNullOrEmpty(updated.PersonDescription))
            {
                person.PersonDescription = updated.PersonDescription;
            }

            return new OkObjectResult(person);
        }

        //get person by id GET
        [HttpGet]
        [FunctionName("GetPersonById")]
        public static IActionResult GetPersonById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "people/{id}")]HttpRequest req, 
            TraceWriter log, string id)
        {
            var person = items.FirstOrDefault(t => t.Id == id);
            if (person == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(person);
        }

        //delete person by id DELETE
        [HttpDelete]
        [FunctionName("DeletePerson")]
        public static IActionResult DeletePerson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "people/{id}")]HttpRequest req, 
            TraceWriter log, string id)
        {
            var person = items.FirstOrDefault(t => t.Id == id);
            if (person == null)
            {
                return new NotFoundResult();
            }
            items.Remove(person);
            return new OkResult();
        }

    }
}
