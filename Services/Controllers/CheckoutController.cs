using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Services.Controllers
{
    public class CheckoutController : Controller
    {
        public ActionResult Index()
        {
            Cart Cart = Session["cart"] != null
                   ? Session["cart"] as Cart
                   : new Cart()
                   {
                       Cliente = new UserEntity(),
                       Produtos = new List<ProductEntity>(),
                       QuantidadePorProduto = new Dictionary<int, int>()
                   };
            return View(Cart);
        }
    }
}