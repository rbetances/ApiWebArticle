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
    public class ArticlesController : ApiController
    {
        private pruebaEntities db = new pruebaEntities();

        // GET api/Articles
        public IEnumerable<Art> GetArticles()
        {
            
            List<Art> listArt = new List<Art>();

            var article = db.Article.Include(a => a.category1);

            foreach (var i in article)
            {
                Art art = new Art()
                {
                    id = i.id,
                    category = i.category1.id,
                    category1 = i.category1.description,
                    title = i.title

                };
                listArt.Add(art);
 
            }

            return listArt.AsEnumerable();
        }

        // GET api/Articles/5
        public Article GetArticle(int id)
        {
            Article article = db.Article.Find(id);
            if (article == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return article;
        }

        // PUT api/Articles/5
        public HttpResponseMessage PutArticle(int id, Article article)
        {
                var original = db.Article.Find(id);

                original.title = article.title;
                original.category = article.category;

                try
                {
                    if (original != null)
                    {
                        db.Entry(original).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);

        }

        // POST api/Articles
        public HttpResponseMessage PostArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Article.Add(article);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, article);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = article.id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Articles/5
        public HttpResponseMessage DeleteArticle(int id)
        {
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Article.Remove(article);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, article);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}