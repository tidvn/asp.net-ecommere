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
            if (cart == null){
               ViewBag.total = 0 ;
            }else{
                ViewBag.total = cart.Sum(item => item.Price*item.Quantity);
            }
            
            
            
            // List<dynamic> list = new List<dynamic>();    

            // if (cart != null)
            // {
            //     foreach (var item in cart)
            //     {
            //         var image = item.ImageData;
            //         dynamic myObject = new System.Dynamic.ExpandoObject();
            //         myObject.Inventory = item.Inventory;
            //         myObject.Image = "data:"+image.FileType+"; base64,"+ @Convert.ToBase64String(image.Data); //; 
            //         myObject.Quantity = item.Quantity;
            //         myObject.Discount = item.Discount;
            //         list.Add(myObject);
            //     }
            //     ViewBag.Cart = list;    
            //     ViewBag.total = cart.Sum(item => item.Inventory.Price * item.Quantity - item.Discount);
            // }
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartAdd item)
        {
            var product = await _ProductService.GetByIdAsync(item.idProduct);
            var image = await _ImageService.GetByIdAsync(product.Images[0]);
            var inventory = await _ProductService.GetInventoryByIdAsync(item.idInventory);


            if (inventory.Quantily <= 0)
               return Json(new { success = false});

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                
                cart.Add(new CartItem
                {  
                    ID = inventory.Id,
                    Name = product.Name,
                    ImageData = image,
                    Inventory = inventory,
                    Quantity = item.quantily,
                    Price = inventory.Price-((inventory.Discount_Percent*inventory.Price)/100),
                    Discount = inventory.Discount_Percent
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(item.idInventory);
                if (index != -1)
                {
                    cart[index].Quantity+= item.quantily;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        ID = inventory.Id,
                        ImageData = image,
                        Inventory = inventory,
                        Quantity = item.quantily,
                        Price = inventory.Price-((inventory.Discount_Percent*inventory.Price)/100),
                        Discount = inventory.Discount_Percent
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
           return Json(new { success = true});
        }
        [HttpPost]
        public IActionResult RemoveFromCart([FromBody] CartItem c)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var item = cart.FirstOrDefault(i => i.ID == c.ID);
            int index = isExist(c.ID);
            if (index != -1 && cart[index].Quantity >= 1){cart.RemoveAt(index);}               
                
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
             return Json(new { success = true});
        }

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
                if (cart[i].ID.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}