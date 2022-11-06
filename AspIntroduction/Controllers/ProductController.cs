using AspIntroduction.Core.Contracts;
using AspIntroduction.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspIntroduction.Controllers
{
    /// <summary>
    /// Web shop products
    /// </summary>
    
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            this.productService = _productService;
        }



        /// <summary>
        /// List all products
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAll();
            ViewData["Title"] = "Products";

            return View(products);
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Supervisor")]
        public IActionResult Add()
        {
            var model = new ProductDto();
            ViewData["Title"] = "Add new products";

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Supervisor")]
        public async Task<IActionResult> Add(ProductDto model)
        {
            ViewData["Title"] = "Add new products";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.Add(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await productService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
