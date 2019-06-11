using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.Models
{
    public class Cart
    {
        public List<ProductEntity> Produtos;
        public UserEntity Cliente;
        public Dictionary<int, int> QuantidadePorProduto;
    }
}