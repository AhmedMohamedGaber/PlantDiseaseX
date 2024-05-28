using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core;
using NToastNotify;
using Microsoft.AspNetCore.Authorization;

namespace Admin.PlantDiseaseX.Controllers
{
    [Authorize]

    public class SeasonController : Controller
    {
        private readonly IToastNotification _toastNotification;

        private readonly IUnitOfWork unitOfWork;

        public SeasonController(IUnitOfWork unitOfWork, IToastNotification toastNotification)
        {
            this._toastNotification = toastNotification;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var Seasons = await unitOfWork.Repository<Season>().GetAllAsync();
            return View(Seasons);
        }



        [HttpPost]
        public async Task<IActionResult> Create(Season season)
        {
            try
            {
                await unitOfWork.Repository<Season>().AddAsync(season);
                await unitOfWork.Complete();
                _toastNotification.AddSuccessToastMessage("Season Created Successfully");
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Name", "Please Enter A New Season");
                return View("index", await unitOfWork.Repository<Season>().GetAllAsync());
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var season = await unitOfWork.Repository<Season>().GetByIdAsync(id);
            unitOfWork.Repository<Season>().Delete(season);
            await unitOfWork.Complete();
            _toastNotification.AddErrorToastMessage("Season Deleted Successfully");

            return RedirectToAction("index");
        }
    }
}
