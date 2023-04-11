using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TDStore.Service;
using TDStore.Models;
using TDStore.Helpers;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Buffers.Text;


namespace TDStore.Areas.Shop.Controllers
{
    [Area("Store")]
    public class CartController : Controller
{
        private readonly ProductService _ProductService;
        private readonly ImageService _ImageService;
        public CartController(ProductService ProductService, ImageService ImageService)
        {
            _ProductService = ProductService;
            _ImageService = ImageService;
        }
        public async Task<IActionResult> Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            
            List<dynamic> list = new List<dynamic>();    

            if (cart != null)
            {
                foreach (var item in cart)
                {
                    var image = item.ImageData;
                    dynamic myObject = new System.Dynamic.ExpandoObject();
                    myObject.Inventory = item.Inventory;
                    myObject.Image = "data:"+image.FileType+"; base64,"+ @Convert.ToBase64String(image.Data); //; 
                    myObject.Quantity = item.Quantity;
                    myObject.Discount = item.Discount;
                    list.Add(myObject);
                }
                ViewBag.Cart = list;    
                ViewBag.total = cart.Sum(item => item.Inventory.Price * item.Quantity - item.Discount);
            }
            return View();
        }

        public async Task<IActionResult> AddToCart(string idProduct,string idInventory,string Quantily)
        {
            var product = await _ProductService.GetByIdAsync(idProduct);
            var image = await _ImageService.GetByIdAsync(product.Images[0]);
            var inventory = await _ProductService.GetInventoryByIdAsync(idInventory);


            if (inventory.Quantily <= 0)
                return RedirectToAction("Index", "Shop");

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                
                cart.Add(new CartItem
                {   Name = product.Name,
                    ImageData = image,
                    Inventory = inventory,
                    Quantity = 1,
                    Discount = 0
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(idInventory);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        ImageData = image,
                        Inventory = inventory,
                        Quantity = 1,
                        Discount = 0
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index","Shop");
        }

      //   public IActionResult RemoveFromCart(string id)
      //   {
      //       List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
      //       var item = cart.FirstOrDefault(i => i.Product.Id == id);
      //       int index = isExist(id);
      //       if (index != -1 && cart[index].Quantity > 1)
      //           cart[index].Quantity--;
      //       SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
      //       return RedirectToAction("Index");
      //   }

      //   public IActionResult subOne(string id)
      //   {
      //       List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
      //       int index = isExist(id);
      //       if (index != -1 && cart[index].Quantity > 1)
      //           cart[index].Quantity--;
      //       SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
      //       return RedirectToAction("Index");
      //   }

      //   public IActionResult addOne(string id)
      //   {
      //       List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
      //       int index = isExist(id);
      //       if (index != -1 && cart[index].Quantity < cart[index].Product.Quantity)
      //           cart[index].Quantity++;
      //       SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

      //       return RedirectToAction("Index");
      //   }
        private int isExist(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Inventory.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}