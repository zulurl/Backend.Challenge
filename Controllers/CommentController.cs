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
    public class CommentController : ControllerBase
    {
        // GET: api/<CommentController>
        [HttpGet]
        public List<Comment> Get()
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
                   

                    return mylist;

                    
                }


            }
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public List<Comment> Get(string id)
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


                using (IDocumentSession session = store.OpenSession())
                {
                    List<Comment> mylist = session.Query<Comment>().ToList();

                    return mylist.Where(x => x.Identificador == id).ToList();



                }


            }
        }

        // POST api/<CommentController>
        [HttpPost]
        public void Post([FromBody] Comment comment)
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

               

                using (IDocumentSession session = store.OpenSession())
                {
                    Comment entity = new Comment
                    {
                        Identificador = comment.Identificador,
                        Texto = comment.Texto,
                        Autor = comment.Autor,
                        Data = comment.Data
                    };

                    session.Store(entity);


                    session.SaveChanges();
                }

               

         

            }
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
