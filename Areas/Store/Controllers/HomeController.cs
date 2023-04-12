﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using TDStore.Areas.Store.Models;
using TDStore.Models;
using TDStore.Models.Repository;
using TDStore.Service;

namespace TDStore.Areas.Store.Controllers;
[Area("Store")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProductService _productService;
    private readonly ImageService _imageService;

    IRepositoryProduct repositoryProduct;
    IRepositoryCart repositoryCart;
    IRepositoryImage repositoryImages;
    IRepositoryOder repositoryOder;


    public HomeController(ILogger<HomeController> logger,ProductService productService,ImageService imageService,
        IRepositoryProduct repositoryProduct,
        IRepositoryCart repositoryCart,
    IRepositoryImage repositoryImages,
    IRepositoryOder repositoryOder
        )
    {
        _logger = logger;
        _productService = productService;
        _imageService = imageService;

        this.repositoryProduct = repositoryProduct;
        this.repositoryCart = repositoryCart;
        this.repositoryImages = repositoryImages;
        this.repositoryOder = repositoryOder;

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
    [HttpGet]   
    public async Task<IActionResult> ListProducts( string name, string id)
    {
        //var data = repositoryProduct.GetAll();
        //return View(data);
        List<Product> lst = await _productService.GetAllAsync();
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

        if (!string.IsNullOrEmpty(name))
        {
            lview.Where(x => x.Name == name).OrderBy(x => x.Name);
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

