using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Model
{
    [Table("pizza")] // Zorg ervoor dat dit overeenkomt met de tabelnaam
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Naam { get; set; }

        [MaxLength(500)]
        public string Beschrijving { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Prijs { get; set; }

        [Column(TypeName = "blob")]
        public byte[] Image { get; set; }

    }

}
