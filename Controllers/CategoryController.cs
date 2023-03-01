using Beml.ECommerce.App.Data;
using Beml.ECommerce.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Beml.ECommerce.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Categories;
            return View(objList);
        }
        //CREATE-GET
        public IActionResult Create()
        {
            return View();
        }
        //CREATE-POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Created Category Successfully!!";
                return RedirectToAction("Index");
            }
            return View();
        }

        // UPDATE  GET
        public IActionResult Update(int id)
        {
            if(id==0)
            {
                return NotFound();
            }
            Category objCategory = _db.Categories.First<Category>(ele => ele.CategoryId == id);
            if (objCategory == null) {
                return NotFound();
            }
            return View(objCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category objCategory)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(objCategory);
                _db.SaveChanges();
                TempData["success"] = "Updated Category Successfully!!";
                return RedirectToAction("Index");
            }
            
            return View();
        }

        //DELETE GET
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Category objCategory = _db.Categories.First<Category>(ele => ele.CategoryId == id);
            if (objCategory == null)
            {
                return NotFound();
            }
            return View(objCategory);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(Category objCat)
        {
            var objCategoryDel = _db.Categories.Find(objCat.CategoryId);
            if (objCategoryDel == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Remove(objCategoryDel);
                _db.SaveChanges();
                TempData["success"] = "Deleted Category Successfully!!";
                return RedirectToAction("Index");
            }    
            return View();
        }
    }
}
