using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiArticles.Models;

namespace WebApiArticles.Controllers
{
    public class CategoriesController : ApiController
    {
        private pruebaEntities db = new pruebaEntities();

        // GET api/Categories
        public IEnumerable<Catego> Getcategories()
        {
            List<Catego> ListCat = new List<Catego>();

            foreach (var i in db.category)
            {
                Catego cat = new Catego() {
                    id = i.id,
                    description = i.description
                };
                ListCat.Add(cat);
            }

            return ListCat.AsEnumerable();
        }

        // GET api/Categories/5
        public category Getcategory(int id)
        {
            category category = db.category.Find(id);
            if (category == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return category;
        }

        // PUT api/Categories/5
        public HttpResponseMessage Putcategory(int id, category category)
        {
            if (ModelState.IsValid && id == category.id)
            {
                db.Entry(category).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Categories
        public HttpResponseMessage Postcategory(category category)
        {
            if (ModelState.IsValid)
            {
                db.category.Add(category);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, category);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = category.id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Categories/5
        public HttpResponseMessage Deletecategory(int id)
        {
            category category = db.category.Find(id);
            if (category == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.category.Remove(category);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}