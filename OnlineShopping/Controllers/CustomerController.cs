using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineShopping.Models;
using OnlineShopping.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShopping.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICartRepository cartRepository;
        private readonly IOrderRepository orderRepository;

        public CustomerController(IProductRepository productRepository, ICartRepository cartRepository,IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.cartRepository = cartRepository;
            this.orderRepository = orderRepository;
        }

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public ActionResult ViewProducts()
        {
            if (HttpContext.Session.GetString("cust") != null)
            {
                var mymodel = new ProductModel();
                mymodel.AllProducts = productRepository.GetAllUser();
                mymodel.Product = new ProductsViewModel();
                return View(mymodel);
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }

        public ViewResult AddtoCart(string id)
        {
            if (HttpContext.Session.GetString("cust") != null)
            {
                ProductDetails productDetails = productRepository.GetProduct(id);
                CartDetails order = new CartDetails
                {
                    ProductID = productDetails.ProductID,
                    ProductName = productDetails.ProductName,
                    PhotoPath = productDetails.PhotoPath,
                    UnitPrice = productDetails.UnitPrice,
                    Quantity = "1",
                    SubTotal = productDetails.UnitPrice
                };

                cartRepository.AddtoCart(order);
                ViewBag.Message = $"Product {order.ProductName} added to Cart.";
                var mymodel = new ProductModel();
                mymodel.AllProducts = productRepository.GetAllUser();
                mymodel.Product = new ProductsViewModel();
                return View("ViewProducts",mymodel);
            }
            else
            {
                return View("login", "account");
            }
        }

        public ActionResult Cart()
        {
            if (HttpContext.Session.GetString("cust") != null)
            {
                var model = new CartViewModel();
                model.order = cartRepository.GetAllOrder();
                return View(model);
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }

        [HttpPost]
        public ActionResult UpdateCart(CartViewModel model)
        {
            if (HttpContext.Session.GetString("cust") != null)
            {
                foreach (var order in model.order)
                {
                    if(order.Quantity==null)
                    {
                        ViewBag.Message = $"Quantity cannot be null.";
                    }
                    else
                    {
                        order.SubTotal = (decimal.Parse(order.Quantity) * decimal.Parse(order.UnitPrice)).ToString();
                    }  
                }
                return View("Cart", model);
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }

        [HttpPost]
        public ActionResult CheckoutCart(CartViewModel model)
        {
            if (HttpContext.Session.GetString("cust") != null)
            {
                int totalqnt = 0;
                decimal orderamt = 0.0m;

                foreach (var order in model.order)
                {
                    if (order.Quantity == null)
                    {
                        ViewBag.Message = $"Quantity cannot be null.";
                        return View("Cart", model);
                    }
                    else
                    {
                        totalqnt = totalqnt + int.Parse(order.Quantity);
                        orderamt = orderamt + (decimal.Parse(order.Quantity) * decimal.Parse(order.UnitPrice));
                    }
                }

                Orders newOrder = new Orders()
                {
                    TotalQunatity = totalqnt.ToString(),
                    OrderAmount = orderamt.ToString(),
                    OrderID = GenerateRandomNo().ToString(),
                    OrderDate = DateTime.Now
                };

                orderRepository.AddOrders(newOrder);

                foreach (var order in model.order)
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        OrderId = newOrder.OrderID,
                        ProductID = order.ProductID,
                        RequiredQunatity = order.Quantity,
                        Amount = (decimal.Parse(order.Quantity) * decimal.Parse(order.UnitPrice)).ToString(),
                    };

                    orderRepository.AddorderDetails(orderDetails);
                }

                cartRepository.ClearCart();

                return Json($"Thank you for Shopping.  Order placed.  Order ID : {newOrder.OrderID}  " +
                    $"Total Quantity: {totalqnt}  Total Amount:  {orderamt}.  Please visit us again.");
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }
    }
}
