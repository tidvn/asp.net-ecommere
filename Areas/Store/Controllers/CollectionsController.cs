using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using TDStore.Areas.Store.Models;
using TDStore.Models;
using TDStore.Service;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace TDStore.Areas.Store.Controllers;
[Area("Store")]
public class CollectionsController : Controller
{
    private readonly ILogger<CollectionsController> _logger;
    private readonly ProductService _productService;
    private readonly ImageService _imageService;



    public CollectionsController(ILogger<CollectionsController> logger,ProductService productService,ImageService imageService)

    {
        _logger = logger;
        _productService = productService;
        _imageService = imageService;

    }

 
    [HttpGet]   
    public async Task<IActionResult> Index(int? page,string? Search)
    {
        //var data = repositoryProduct.GetAll();
        //return View(data);
       List<Product> lst = new List<Product>();
        if ( Search != null){
         lst = await _productService.SearchProduct(Search);
        }else{
         lst = await _productService.GetAllAsync();
        }
        List<ProductView> lview = new List<ProductView>();
        foreach (var item in lst)
        {
            List<ImageData>? limg = new List<ImageData>();
            foreach (var img in item.Images)
            {
                limg.Add(await _imageService.GetByIdAsync(img));
            }
            lview.Add(new ProductView()
            {
                Id = item.Id,
                Name = item.Name,
                Details = item.Details,
                Images = limg,
                List_Specifications = item.Specifications,
                Features = item.Features,
                Category = await _productService.GetAllCategoryOfProduct(item.Id),
                List_Inventory = await _productService.GetAllInventoryOfProduct(item.Id)
            });

        }

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = lview.ToPagedList(pageNumber, 25);// will only contain 25 products max because of the pageSize
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();


    }
   

}

