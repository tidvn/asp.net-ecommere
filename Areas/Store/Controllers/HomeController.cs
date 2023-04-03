using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TDStore.Areas.Store.Models;
using TDStore.Models;
using TDStore.Service;

namespace TDStore.Areas.Store.Controllers;
[Area("Store")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProductService _productService;
    private readonly ImageService _imageService;

    public HomeController(ILogger<HomeController> logger,ProductService productService,ImageService imageService)
    {
        _logger = logger;
        _productService = productService;
        _imageService = imageService;
    }

    public async Task<IActionResult>  Index()
    {
        List<Product> lst = await _productService.GetAllAsync();
        List<ProductView> lview = new List<ProductView>();
        // List<Product_Category> lcate = new List<Product_Category>();
        // List<Product_Inventory> linv= new List<Product_Inventory>();

        foreach ( var item in lst){
            List<ImageData>? limg = new List<ImageData>();
            foreach (var img in item.Images){
                limg.Add(await _imageService.GetByIdAsync(img));
            }
         lview.Add(new ProductView(){
            Id= item.Id,
            Name= item.Name,
            Details=item.Details,
            Images=limg,
            List_Specifications= item.Specifications,
            Features=item.Features,
            Category = await _productService.GetAllCategoryOfProduct(item.Id),
            List_Inventory =  await _productService.GetAllInventoryOfProduct(item.Id)
         });

        }

        return View(lview);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

