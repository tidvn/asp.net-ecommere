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
    private readonly ImageService _imageService;

    public ProductController(
        ILogger<ProductController> logger,
        ProductService productService,
        ImageService imageService
    
    )
    {
        _productService=productService;
        _imageService=imageService;
        _logger = logger;
    }

    public async Task<IActionResult> AddProduct()
    {
        List<Product_Category> lst = await _productService.GetAllCategoryAsync();
        return View(lst);
    }
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] Product_Category category)
    {
        if (ModelState.IsValid)
        {
            await  _productService.CreateCategoryAsync(category);
            return Json(new { success = true,category= category });
        }

        return Json(new { success = false });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddProduct(ProductAddModel ModelData, List<IFormFile> ImageProduct)
    {
        List<String> listImage = new List<String>();
        List<String> listInventory = new List<String>();
        foreach (var file in ImageProduct)
            {
                var fileModel = new ImageData
                {
                    FileType = file.ContentType,
                    Extension = Path.GetExtension(file.FileName),
                    Title= Path.GetFileNameWithoutExtension(file.FileName)
                };
              using (var dataStream = new MemoryStream())
              {
                await file.CopyToAsync(dataStream);
                fileModel.Data = dataStream.ToArray();
              }
             
              await _imageService.CreateAsync(fileModel);
              if(fileModel.Id !=""){
                listImage.Add(fileModel.Id);
            }
            }
        //

        foreach (var inv in ModelData.List_Inventory){
            await _productService.CreateInventoryAsync(inv);
            if(inv.Id !=""){
                listInventory.Add(inv.Id);
            }
            
        }
        Product p = new Product(){
            Name= ModelData.Name,
            Details=ModelData.Details,
            Specifications=ModelData.List_Specifications,
            Features=ModelData.Features,
            Category= ModelData.Category,
            Images=listImage,
            Inventory=listInventory
        };
        
        try{
            await _productService.CreateAsync(p);
            
        }catch{
            System.Diagnostics.Debug.WriteLine("error add product");
        }
        List<Product_Category> lst = await _productService.GetAllCategoryAsync();
        return View(lst);
        
    }
        
}

