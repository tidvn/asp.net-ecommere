using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TDStore.Service;
using TDStore.Models;
using TDStore.Helpers;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Buffers.Text;


namespace TDStore.Areas.Store.Controllers
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
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

            List<dynamic> list = new List<dynamic>();

            if (cart != null)
            {
                foreach (var item in cart)
                {
                    var image = item.ImageData;
                    dynamic myObject = new System.Dynamic.ExpandoObject();
                    myObject.Image = "data:" + image.FileType + "; base64," + @Convert.ToBase64String(image.Data);
                    myObject.Name = myObject.Name;
                    myObject.Quantity = item.Quantity;
                    myObject.Price = item.Price;
                    myObject.Discount = item.Discount;
                    list.Add(myObject);
                }
                ViewBag.Cart = list;
                ViewBag.total = cart.Sum(item => item.Inventory.Price * item.Quantity - item.Discount);
            }
            return View(cart);
        }
        
        public async Task<IActionResult> AddToCart(string idProduct, string idInventory, string Quantily)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            var product = await _ProductService.GetByIdAsync(idProduct);
            var image = await _ImageService.GetByIdAsync(product.Images[0]);
            var inventory = await _ProductService.GetInventoryByIdAsync(idInventory);

            if (inventory.Quantily <= 0)
                return RedirectToAction("Index", "Shop");

            if (HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();

                cart.Add(new CartItem
                {
                    ImageData = image,
                    Name = product.Name,
                    Quantity = 1,
                    Inventory = inventory,
                    Price = 0,
                    Discount = 0
                });
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
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
                        Name = product.Name,
                        Quantity = 1,
                        Inventory = inventory,
                        Price = 0,
                        Discount = 0
                    });
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);
                code = new { Success = true, msg = "Thêm sản phẩm vào giở hàng thành công!", code = 1, Count = cart.Count };

            }
            //return RedirectToAction("Index", "Shop");
            return Json(code);

        }

        public IActionResult RemoveFromCart(string id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            var item = cart.FirstOrDefault(i => i.Name == id);
            int index = isExist(id);
            if (index != -1 && cart[index].Quantity > 1)
                cart[index].Quantity--;
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult subOne(string id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            int index = isExist(id);
            if (index != -1 && cart[index].Quantity > 1)
                cart[index].Quantity--;
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult addOne(string id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            int index = isExist(id);
            if (index != -1 && cart[index].Quantity < cart[index].Quantity)
                cart[index].Quantity++;
            HttpContext.Session.SetObjectAsJson("cart", cart);

            return RedirectToAction("Index");
        }
        private int isExist(string id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
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