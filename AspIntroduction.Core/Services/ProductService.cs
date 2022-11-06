using AspIntroduction.Core.Contracts;
using AspIntroduction.Core.Data;
using AspIntroduction.Core.Data.Models;
using AspIntroduction.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIntroduction.Core.Services
{
    /// <summary>
    /// Manipulates product data
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IConfiguration config;

        private readonly ApplicationDbContext context;

        /// <summary>
        /// IoC
        /// </summary>
        /// <param name="config">Application configuration</param>
        public ProductService(
            IConfiguration _config,
           ApplicationDbContext _context)
        {
            config = _config;
            context = _context;
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="productDto">Product model</param>
        /// <returns></returns>
        public async Task Add(ProductDto productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity
            };

            await context.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                product.IsActive = false;

                await context.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>List of products</returns>
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return await context.Products
                .Where(p => p.IsActive)
                .Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,  
                Price = p.Price,
                Quantity = p.Quantity
            }).ToListAsync();
        }
    }
}
