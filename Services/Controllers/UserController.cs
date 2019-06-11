using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Services.Models;
using Services.Repository;

namespace Services.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            using (Repository<UserEntity> rep = new RepositoryUser())
            {
                return View(rep.GetAll().ToList());
            }
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }    
            using (Repository<UserEntity> rep =new RepositoryUser())
            {
                UserEntity userEntity = rep.Find(id);
                if (userEntity == null)
                {
                    return HttpNotFound();
                }
                return View(userEntity);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,First_name,Last_name,CPF,Email,Login,Password,Created,Updated")] UserEntity userEntity)
        {
            if (ModelState.IsValid)
            {
                using (Repository<UserEntity> rep = new RepositoryUser())
                {
                    rep.Add(userEntity);
                    rep.SaveAll();
                }
                return RedirectToAction("Logar", new { userEntity.Login, userEntity.Password});
            }

            return View(userEntity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            using (Repository<UserEntity> rep = new RepositoryUser())
            {
                UserEntity userEntity = rep.Find(id);
                if (userEntity == null)
                {
                    return HttpNotFound();
                }
                return View(userEntity);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,First_name,Last_name,CPF,Email,Login,Password,Created,Updated")] UserEntity userEntity)
        {
            if (ModelState.IsValid)
            {
                using (Repository<UserEntity> rep = new RepositoryUser())
                {
                    rep.Add(userEntity);
                    rep.SaveAll();
                }
                return RedirectToAction("Index");
            }
            return View(userEntity);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (Repository<UserEntity> rep = new RepositoryUser())
            {
                UserEntity userEntity = rep.Find(id);
                if (userEntity == null)
                {
                    return HttpNotFound();
                }
                return View(userEntity);
            }            
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (Repository.RepositoryUser rep = new Repository.RepositoryUser())
            {
                rep.Delete((u => u.ID == id));
                rep.SaveAll();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Login(int? id)
        {
            ViewBag.id = id;
            return View(id);
        }

        public ActionResult Logar(string Login, string Password , int? id)
        {   
            try
            {
                using (RepositoryUser rep = new RepositoryUser())
                {
                    Func<UserEntity, bool> predicate = (u => u.Login == Login && u.Password == Password);
                    UserEntity user = rep.Get(predicate).FirstOrDefault();
                    if (user == null)
                    {
                        ViewData["Error"] = "Login e/ou senha inválidos.";
                        return View("Login");
                    }
                    user.Password = String.Empty;
                    Session["ID"] = user.ID;
                    Session["Login"] = user.Login;
                    Session["Nome"] = user.First_name + " " + user.Last_name;
                    Session["Logado"] = "logado";
                    if (id != null) {
                        return RedirectToAction("AdicionarCarrinho", "Cart", new { id });
                    }
                    return RedirectToAction("Index", "Product");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Login e/ou senha incorretos. Verifique!";
                return View("Index");
            }
        }

        public ActionResult Exit()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Product");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
