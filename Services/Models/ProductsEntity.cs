using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Services.Models
{
    [Table("Products")]
    public class ProductEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DisplayName("Nome do produto")]
        public string Name_Product { get; set; }
        [Required]
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Preço")]
        public double Price { get; set; }
        [Required]
        [DisplayName("Url da imagem")]
        public string Link_Image { get; set; }
        [Required]
        [DisplayName("Categoria")]
        public string Category { get; set; }


        public override bool Equals(object obj)
        {
            ProductEntity temp = obj as ProductEntity;
            if (temp.ID == this.ID)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1219474093;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name_Product);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Link_Image);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Category);
            return hashCode;
        }
    }
}