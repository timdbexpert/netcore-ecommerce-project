using Services.Models;
using Services.Repository;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Services.Controllers
{
    public class CartController : Controller
    {
        private static Cart Cart = new Cart();

        public ActionResult Index()
        {
            return View(Cart);
        }

        public ActionResult AdicionarQuantidade(int id)
        {
            if (Cart.QuantidadePorProduto[id] < 50)
            {
                Cart.QuantidadePorProduto[id] = Cart.QuantidadePorProduto[id] + 1;
            }
            return View("Index", Cart);
        }

        public ActionResult RemoverQuantidade(int id)
        {
            if (Cart.QuantidadePorProduto[id] > 1)
            {
                Cart.QuantidadePorProduto[id] = Cart.QuantidadePorProduto[id] - 1;
            }
            else
            {
                RemoverItem(id);
            }
            return View("Index", Cart);
        }

        public ActionResult RemoverItem(int id)
        {
            using (Repository<ProductEntity> rep = new RepositoryProduct())
            {
                ProductEntity product = rep.Find(id);
                Cart.Produtos.Remove(product);
                Cart.QuantidadePorProduto.Remove(product.ID);
            }
            return View("Index", Cart);
        }

        public ActionResult AdicionarCarrinho(int? id)
        {
            Cart = Session["cart"] != null
                ? Session["cart"] as Cart
                : new Cart()
                {
                    Cliente = new UserEntity(),
                    Produtos = new List<ProductEntity>(),
                    QuantidadePorProduto = new Dictionary<int, int>()
                };
            if (Session == null || Session["ID"] == null)
            {
                return RedirectToAction("Login", "User", new { id });
            }

            using (Repository<ProductEntity> rep = new RepositoryProduct())
            {
                ProductEntity product = rep.Find(id);
                if (Cart.Produtos.Contains(product))
                {
                    if (Cart.QuantidadePorProduto[product.ID] < 50)
                    {
                        Cart.QuantidadePorProduto[product.ID] = Cart.QuantidadePorProduto[product.ID] + 1;
                    }
                }
                else
                {
                    Cart.Produtos.Add(product);
                    Cart.QuantidadePorProduto.Add(product.ID, 1);
                }
            }
            using (Repository<UserEntity> repUser = new RepositoryUser())
            {
                Cart.Cliente = repUser.Find(int.Parse(Session["ID"].ToString()));
            }

            Session["cart"] = Cart;
            return RedirectToAction("Index", "Product");
        }
    }
}