using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PlantDiseaseX.Core;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Repository;
namespace Admin.PlantDiseaseX.Controllers
{
    [Authorize]

    public class CategoryController : Controller
    {
        private readonly IToastNotification _toastNotification;

        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork , IToastNotification toastNotification)
        {
            this._toastNotification = toastNotification;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var Categories = await unitOfWork.Repository<Plantcategory>().GetAllAsync();
            return View(Categories);
        }



        [HttpPost]
        public async Task<IActionResult> Create(Plantcategory category)
        {
            try
            {
                await unitOfWork.Repository<Plantcategory>().AddAsync(category);
                await unitOfWork.Complete();
                _toastNotification.AddSuccessToastMessage("Category Created Successfully");
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Name", "Please Enter A New Category");
                return View("index", await unitOfWork.Repository<Plantcategory>().GetAllAsync());
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var category = await unitOfWork.Repository<Plantcategory>().GetByIdAsync(id);
            unitOfWork.Repository<Plantcategory>().Delete(category);
            await unitOfWork.Complete();

            _toastNotification.AddErrorToastMessage("Category Deleted Successfully");

            return RedirectToAction("index");
        }
    }
}
