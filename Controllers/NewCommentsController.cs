using Backend.Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewCommentsController : ControllerBase
    {
        // GET: api/<NewCommentsController>
        [HttpGet]
        public Comment Get()
        {
            using (IDocumentStore store = new DocumentStore
            {
                Urls = new[]
             {
               "http://localhost:8080"
            },
                Database = "MyNewDatabase",
                Conventions = { }
            })
            {
                store.Initialize();

                var exists = store.Maintenance.Server.Send(new GetDatabaseRecordOperation("MyNewDatabase"));

                if (exists == null) store.Maintenance.Server.Send(
                                    new CreateDatabaseOperation(new DatabaseRecord("MyNewDatabase"))); ;

                using (IDocumentSession session = store.OpenSession())
                {
                    List<Comment> mylist = session.Query<Comment>().ToList();

                    var last = (from x in mylist select x).LastOrDefault();
                    

                    return last;




                }


            }
        }

        // GET api/<NewCommentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NewCommentsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NewCommentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NewCommentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
