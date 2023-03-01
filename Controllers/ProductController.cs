using Beml.ECommerce.App.Data;
using Beml.ECommerce.App.Models;
using Beml.ECommerce.App.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Beml.ECommerce.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        //Below Variable is used to access the Images folders which is inside the wwwroot
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        //ProductList - GET
        public IActionResult IndexOld()
        {
            IEnumerable<Product> objList = _db.Products;

            foreach(var obj in objList)
            {
                obj.Category = _db.Categories.FirstOrDefault(u =>u.CategoryId == obj.CategoryId);
            }
            return View(objList);
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Products.Include(i=>i.Category).Include(ele=>ele.ApplicationType);           
            return View(objList);
        }
        //CREATE-GET
        /*
        public IActionResult UpsertVBOrVD(int? id)
        {
            //1 way to display the dropdownlist in view is selectListItem(Microsoft.AspNet.Core.Mvc.Rendering)
            // U can pass the data of dropdownlist using ViewBag or ViewData C->V

            //Ideally ViewBag or ViewData its not a recommended solution for using the dropdown field as 
            //It is advisable that your view should be strongly typed but when we use ViewData/ViewBag it breaks that.

            IEnumerable<SelectListItem> objCategoyList = _db.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CategoryId.ToString()
            });
            //ViewBag.CategoryDropDown = objCategoyList;

            ViewData["CategoryDropDown"] = objCategoyList;

            Product objProd = new Product();
            if (id == null)
            {
                //Create
                return View();
            }
            else
            {
                objProd = _db.Products.First(el => el.ProductId == id);
                return View(objProd);
            }

        }
        */


        public IActionResult Upsert(int? id)
        {
            ProductVM objProductVM = new ProductVM()
            {
                Product = new Product(),
                CategoryListItem = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CategoryId.ToString()
                }),
                ApplicationListItem= _db.ApplicationTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ApplicationId.ToString()
                })
            };
                        
            if (id == null)
            {
                return View(objProductVM);
            }
            else
            {
                objProductVM.Product = _db.Products.First(el => el.ProductId == id);
                return View(objProductVM);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM objVM)
        {
                  //Fetch Image
                var files = HttpContext.Request.Form.Files;
                //Important
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (objVM.Product.ProductId == 0)
                {
                    //Create
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);
                    objVM.Product.ShortDesc = objVM.Product.Description.Substring(0,12);
                //copy to the location
                using (var filestream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    objVM.Product.ImagePath = fileName + extension;

                    _db.Products.Add(objVM.Product);
                TempData["success"] = "Created Product Successfully!!";

            }
                else
                {
                //Update
                var objFromDb = _db.Products.AsNoTracking().FirstOrDefault(u => u.ProductId == objVM.Product.ProductId);
                //Remove the existing file and create the new file
                if (files.Count > 0)
                {
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);
                    objVM.Product.ShortDesc = objVM.Product.Description.Substring(0, 15);
                    var oldFile = Path.Combine(upload, objFromDb.ImagePath);

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    objVM.Product.ShortDesc = objVM.Product.Description.Substring(0, 12);
                    objVM.Product.ImagePath = fileName + extension;
                }
                else
                {
                    objVM.Product.ImagePath = objFromDb.ImagePath;
                }
                _db.Products.Update(objVM.Product);
                TempData["success"] = "Updated Product Successfully!!";
            }
                _db.SaveChanges();
                return RedirectToAction("Index");
          
        }


        #region "Delete"

        public IActionResult Delete(int? Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }

            Product objProduct = _db.Products.Include(i => i.Category).First(el => el.ProductId == Id);
            if (objProduct == null)
            {
                return NotFound();
            }
            return View(objProduct);
        
        }
        #endregion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? ProductId)
        {
            var obj = _db.Products.Find(ProductId);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, obj.ImagePath);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }


            _db.Products.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Deleted Product Successfully!!";
            return RedirectToAction("Index");
        }
    }
}
