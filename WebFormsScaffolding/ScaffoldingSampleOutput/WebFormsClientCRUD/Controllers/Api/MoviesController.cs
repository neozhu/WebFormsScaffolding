using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebFormsClientCRUD.Filters;
using WebFormsClientCRUD.Models;

namespace WebFormsClientCRUD.Controllers.Api
{
    public class MoviesController : ApiController
    {

        private IGenericRepository repository = new GenericRepository(new MoviesContext());

        // GET api/<controller>
        public HttpResponseMessage GetMovies()
        {
            var movies = repository.Query<Movie>().ToList();
            return Request.CreateResponse(movies);
        }

        // GET api/<controller>/5
        public string GetMovie(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [ValidateModel]
        public HttpResponseMessage PostMovie(Movie movieToCreate)
        {
            repository.Add<Movie>(movieToCreate);
            repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, movieToCreate);
        }

        // PUT api/<controller>/5
        [ValidateModel]
        public HttpResponseMessage Put(Movie movieToUpdate)
        {
            var original = repository.Find<Movie>(movieToUpdate.Id);
            original.Title = movieToUpdate.Title;
            original.Director = movieToUpdate.Director;
            original.TicketPrice = movieToUpdate.TicketPrice;
            repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, original);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}