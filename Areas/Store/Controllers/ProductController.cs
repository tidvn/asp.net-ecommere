using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TDStore.Areas.Store.Models;
using TDStore.Models;
using TDStore.Service;

namespace TDStore.Areas.Store.Controllers;
[Area("Store")]
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly ProductService _productService;
    private readonly ImageService _imageService;

    public ProductController(ILogger<ProductController> logger,ProductService productService,ImageService imageService)
    {
        _logger = logger;
        _productService = productService;
        _imageService = imageService;
    }

    public async Task<IActionResult> Details(string id)
    {
        Product item = await _productService.GetByIdAsync(id);
        if(item == null){
               return RedirectToAction("Index", "Home");
        }
        List<ImageData>? limg = new List<ImageData>();
        foreach (var img in item.Images){
            limg.Add(await _imageService.GetByIdAsync(img));
        }
        ProductView lview = new ProductView(){
            Id= item.Id,
            Name= item.Name,
            Details=item.Details,
            Images=limg,
            List_Specifications= item.Specifications,
            Features=item.Features,
            Category = await _productService.GetAllCategoryOfProduct(item.Id),
            List_Inventory =  await _productService.GetAllInventoryOfProduct(item.Id)
         };

        return View(lview);
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

