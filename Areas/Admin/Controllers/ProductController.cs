using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TDStore.Areas.Admin.Models;
using TDStore.Models;
using TDStore.Service;

namespace TDStore.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    
    private readonly ILogger<ProductController> _logger;
    private readonly ProductService _productService;

    public ProductController(
        ILogger<ProductController> logger,
        ProductService productService
    
    )
    {
        _productService=productService;
        _logger = logger;
    }

    public IActionResult AddProduct()
    {
       
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(Dictionary<string,string>  myDictionary)
    {
        foreach(KeyValuePair<string, string> entry in myDictionary)
        {
            System.Diagnostics.Debug.WriteLine(entry.Key,entry.Value);
        }
       
       // await _productService.CreateAsync(viewModel.Product, viewModel.Inventory, viewModel.Category);
        return View();
    }
    public IActionResult Add()
    {
        return View();
    }

    // POST: Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(ProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var product = new ProductT
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                Inventories = viewModel.Inventories
            };

            

            return RedirectToAction(nameof(Index));
        }

        return View(viewModel);
    }
    

    
}

