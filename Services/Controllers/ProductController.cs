using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Services.Models;
using Services.Repository;

namespace Services.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()

        {
            using (Repository<ProductEntity> rep = new RepositoryProduct())
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
            using (Repository<ProductEntity> rep = new RepositoryProduct())
            {
                ProductEntity productEntity = rep.Find(id);
                if (productEntity == null)
                {
                    return HttpNotFound();
                }
                return View(productEntity);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string Search)
        {
            using (Repository<ProductEntity> rep = new RepositoryProduct())
            {
                return View("Index", rep.Get((p => p.Name_Product.Contains(Search) || p.Description.Contains(Search) || p.Category.Contains(Search) || p.Link_Image.Contains(Search))).ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name_Product,Description,Price,Link_Image,Category")] ProductEntity productEntity)
        {
            if (ModelState.IsValid)
            {
                using (Repository<ProductEntity> rep = new RepositoryProduct())
                {
                    rep.Add(productEntity);
                    rep.SaveAll();
                }
                return RedirectToAction("Index");
            }
            return View(productEntity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (Repository<ProductEntity> rep = new RepositoryProduct())
            {
                ProductEntity productEntity = rep.Find(id);
                if (productEntity == null)
                {
                    return HttpNotFound();
                }
                return View(productEntity);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name_Product,Description,Price,Link_Image,Category")] ProductEntity productEntity)
        {
            if (ModelState.IsValid)
            {
                using (Repository<ProductEntity> rep = new RepositoryProduct())
                {
                    rep.Update(productEntity);
                    rep.SaveAll();
                }
                return RedirectToAction("Index");
            }
            return View(productEntity);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (Repository<ProductEntity> rep = new RepositoryProduct())
            {
                ProductEntity productEntity = rep.Find(id);
                if (productEntity == null)
                {
                    return HttpNotFound();
                }
                return View(productEntity);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (Repository<ProductEntity> rep = new Repository<ProductEntity>())
            {
                rep.Delete((p => p.ID == id));
                rep.SaveAll();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
