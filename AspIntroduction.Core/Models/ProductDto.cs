using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIntroduction.Core.Models
{
    /// <summary>
    /// Product model
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Product price
        /// </summary>
        [Range(0,int.MaxValue)]
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity in stock
        /// </summary>
        [Range(0,int.MaxValue)]
        public int Quantity { get; set; }
    }
}
