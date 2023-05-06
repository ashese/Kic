using KicWeb.DAL;
using KicWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KicWeb.Controllers
{
    public class ServiceCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.ServiceCategoryRepository.GetServiceCategories());
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
                _unitOfWork.ServiceCategoryRepository.AddServiceCategory(serviceCategory);
                _unitOfWork.Commit();
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
            var serviceFromDb = _unitOfWork.ServiceCategoryRepository.GetServiceCategoryById(id);

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
                _unitOfWork.ServiceCategoryRepository.UpdateServiceCategory(serviceCategory);
                _unitOfWork.Commit();
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
            var serviceFromDb = _unitOfWork.ServiceCategoryRepository.GetServiceCategoryById(id);

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
            var serviceFromDb = _unitOfWork.ServiceCategoryRepository.GetServiceCategoryById(id);
            _unitOfWork.Commit();
            if (serviceFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.ServiceCategoryRepository.RemoveServiceCategory(serviceFromDb);
            TempData["Success"] = "Service Category deleted successfully!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.ServiceCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
