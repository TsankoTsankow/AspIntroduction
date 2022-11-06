using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIntroduction.Core.Data.Models
{
    [Comment("Products to sell")]
    public class Product
    {
        [Key]
        [Comment("Primary key")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [Comment("Product name")]
        public string Name { get; set; }

        [Required]
        [Comment("Product price")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Number of products in stock")]
        public int Quantity { get; set; }

        [Required]
        [Comment("Product is active or not")]
        public bool IsActive { get; set; } = true;

    }
}
