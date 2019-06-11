using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    [Table("Users")]
    public partial class UserEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Nome")]
        public string First_name { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Sobrenome")]
        public string Last_name { get; set; }
        [Required]
        [StringLength(11)]
        [Index(IsUnique = true)]
        [DisplayName("CPF")]
        public string CPF { get; set; }
        [Required]
        [StringLength(70)]
        [Index(IsUnique = true)]
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(25)]
        [Index(IsUnique = true)]
        [DisplayName("Login")]
        public string Login { get; set; }
        [Required]
        [StringLength(20)]
        [DisplayName("Senha")]
        public string Password { get; set; }
    }
}
