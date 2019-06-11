using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        [Route("GetAll")]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            using (RepositoryUser rep = new RepositoryUser())
            {
                return request.CreateResponse<ICollection<UserEntity>>(HttpStatusCode.Accepted, rep.GetAll().ToList());
            }
        }

        [AcceptVerbs("GET")]
        [Route("Find/{id}")]
        public HttpResponseMessage Find(HttpRequestMessage request, int id)
        {
            UserEntity user;
            try
            {
                using (RepositoryUser rep = new RepositoryUser())
                {
                    user = rep.Find(id);
                    if (user != null)
                    {
                        return request.CreateResponse<UserEntity>(HttpStatusCode.Accepted, user);
                    }
                    else
                    {
                        return request.CreateResponse<string>(HttpStatusCode.NotFound, "Não foi possível localizar usuário!");
                    }
                }
            }
            catch (Exception e)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Route("Add")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage Save(HttpRequestMessage request, UserEntity user)
        {
            try
            {
                using (RepositoryUser rep = new RepositoryUser())
                {
                    rep.Add(user);
                    rep.SaveAll();
                }
                return request.CreateResponse(HttpStatusCode.Created, user);
            }
            catch (Exception e)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Route("Update")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(HttpRequestMessage request, UserEntity user)
        {
            try
            {
                using (RepositoryUser rep = new RepositoryUser())
                {
                    rep.Update(user);
                    rep.SaveAll();
                    return request.CreateResponse(HttpStatusCode.Accepted, user);
                }
            }
            catch (Exception e)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Route("Delete/{id}")]
        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                using (RepositoryUser rep = new RepositoryUser())
                {
                    rep.Delete((p => p.ID == id));
                    rep.SaveAll();
                    return request.CreateResponse(HttpStatusCode.Accepted);
                }
            }
            catch (Exception e)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
