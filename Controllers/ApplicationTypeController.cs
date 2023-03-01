using Beml.ECommerce.App.Data;
using Beml.ECommerce.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beml.ECommerce.App.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationTypeController(ApplicationDbContext db) {
            _db = db;
        }

        //Get-Index
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList= _db.ApplicationTypes;
            return View(objList);
        }

        #region "CREATE"
        //Get-Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Get-Create
        public IActionResult Create(ApplicationType objApplicationType)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationTypes.Add(objApplicationType);
                _db.SaveChanges();
                TempData["success"] = "Created Application Type Successfully!!";
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region "Update"
        //GET-Update

        public IActionResult Update(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }
            ApplicationType objApplication = _db.ApplicationTypes.First<ApplicationType>(ele => ele.ApplicationId == id);
            if (objApplication == null)
            {
                return NotFound();
            }
            return View(objApplication);                  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationTypes.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Updated Application Type Successfully!!";
                return RedirectToAction("Index");

            }
            return View();
        }
        #endregion

        #region "Delete"
        //GET-Update

        public IActionResult Delete(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }
            ApplicationType objApplication = _db.ApplicationTypes.First<ApplicationType>(ele => ele.ApplicationId == id);
            if (objApplication == null)
            {
                return NotFound();
            }
            return View(objApplication);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationTypes.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Deleted Application Type Successfully!!";
                return RedirectToAction("Index");   

            }
            return View();
        }
        #endregion

    }
}
