using KicWeb.Data;
using KicWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KicWeb.Controllers
{
    public class ServiceCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ServiceCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ServiceCategory> categoryList = _db.ServiceCategories;
            return View(categoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceCategory serviceCategory)
        {
            if (serviceCategory.Name.Equals(serviceCategory.DisplayOrder.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("CustomError", "The DisplayName cannot match the display order!");
            }
            if (serviceCategory != null && ModelState.IsValid)
            {
                _db.ServiceCategories.Add(serviceCategory);
                _db.SaveChanges();
                TempData["Success"] = "Service Category created successfully!";                
                return RedirectToAction("Index");
            }
            return View(serviceCategory);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var serviceFromDb = _db.ServiceCategories.Find(id);

            if (serviceFromDb == null)
            {
                return NotFound();
            }
            return View(serviceFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServiceCategory serviceCategory)
        {
            if (serviceCategory.Name.Equals(serviceCategory.DisplayOrder.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("CustomError", "The DisplayName cannot match the display order!");
            }
            if (serviceCategory != null && ModelState.IsValid)
            {
                _db.ServiceCategories.Update(serviceCategory);
                _db.SaveChanges();
                TempData["Success"] = "Service Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View(serviceCategory);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var serviceFromDb = _db.ServiceCategories.Find(id);

            if (serviceFromDb == null)
            {
                return NotFound();
            }
            return View(serviceFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteService(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var serviceFromDb = _db.ServiceCategories.Find(id);
            if (serviceFromDb == null)
            {
                return NotFound();
            }
            _db.ServiceCategories.Remove(serviceFromDb);
            TempData["Success"] = "Service Category deleted successfully!";
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
