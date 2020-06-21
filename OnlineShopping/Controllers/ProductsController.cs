using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Models;
using OnlineShopping.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShopping.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ProductsController(IProductRepository productRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.productRepository = productRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
       public IActionResult Product()
        {
            if (HttpContext.Session.GetString("admin") !=null)
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

        public IActionResult Add(ProductModel model,string id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (id != null && model.Product.ProductName != null)
                {
                    string uniqueFileName = ProcessUploadFile(model);

                    ProductDetails productDetails = new ProductDetails()
                    {
                        ProductID = id,
                        ProductName = model.Product.ProductName,
                        UnitPrice = model.Product.UnitPrice,
                        Quantity = model.Product.Quantity,
                        PhotoPath = uniqueFileName,
                    };
                    productRepository.AddProduct(productDetails);
                    model.AllProducts = productRepository.GetAllUser();
                    return RedirectToAction("product", model);
                }

                return RedirectToAction("product");
            }
            else
            {
                return RedirectToAction("login", "account");
            }
            
        }

        public IActionResult Search(string id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                var model = new ProductModel();
                model.AllProducts = productRepository.GetAllUser();
                ProductDetails productDetails = productRepository.GetProduct(id);

                if (id == null || productDetails == null)
                {
                    ViewBag.Message = "No matching product with given Product ID in directory." ;
                    return View("product", model);
                }
                else
                {
                    model.Product = new ProductsViewModel
                    {
                        ProductID = productDetails.ProductID,
                        ProductName = productDetails.ProductName,
                        UnitPrice = productDetails.UnitPrice,
                        Quantity = productDetails.Quantity,
                        ExistingPhotoPath = productDetails.PhotoPath,
                    };
                    return View("product", model);
                }

            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }

        [HttpPost]
        public IActionResult Update(ProductModel model, string id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                ProductDetails product = productRepository.GetProduct(id);
                if (id==null || product==null)
                {
                    ViewBag.Message = "No matching product with given Product ID in directory.";
                    model.AllProducts = productRepository.GetAllUser();
                    return View("product", model);
                }
                else
                {
                    model.Product.ProductID = id;
                    product.ProductName = model.Product.ProductName;
                    product.UnitPrice = model.Product.UnitPrice;
                    product.Quantity = model.Product.Quantity;
                    if (model.Product.Photo != null)
                    {
                        if (model.Product.ExistingPhotoPath != null)
                        {
                            string filepath = Path.Combine(hostingEnvironment.WebRootPath, "ProductImage", model.Product.ExistingPhotoPath);
                            System.IO.File.Delete(filepath);
                        }
                        product.PhotoPath = ProcessUploadFile(model);
                    }
                    productRepository.UpdateProduct(product);
                    model.AllProducts = productRepository.GetAllUser();
                    return RedirectToAction("product", model);
                }
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }

        public IActionResult Delete(string id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                ProductDetails product = productRepository.GetProduct(id);
                var model = new ProductModel();
                model.AllProducts = productRepository.GetAllUser();
                if (id == null || product==null)
                {
                    ViewBag.Message = "No matching product with given Product ID in directory.";
                    return View("product", model);
                }
               else
                {
                    if (product.PhotoPath != null)
                    {
                        string picloc = Path.Combine(hostingEnvironment.WebRootPath, "ProductImage",product.PhotoPath);
                        System.IO.File.Delete(picloc);
                    }
                    productRepository.DeleteProduct(product);
                    model.AllProducts = productRepository.GetAllUser();
                    return View("product", model);
                }
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }

        private string ProcessUploadFile(ProductModel model)
        {
            string uniqueFileName = null;
            if (model.Product.Photo != null)
            {
                string productFolder = Path.Combine(hostingEnvironment.WebRootPath, "ProductImage");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Product.Photo.FileName;
                string filepath = Path.Combine(productFolder, uniqueFileName);
                model.Product.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }

            return uniqueFileName;
        }
    }
}
