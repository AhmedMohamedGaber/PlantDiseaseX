using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PlantDiseaseX.Core;
using PlantDiseaseX.Core.Entities;

namespace Admin.PlantDiseaseX.Controllers
{
    [Authorize]

    public class ContactUsController : Controller
    {

        private readonly IToastNotification _toastNotification;

        private readonly IUnitOfWork unitOfWork;


        public ContactUsController(IUnitOfWork unitOfWork, IToastNotification toastNotification)
        {
            this._toastNotification = toastNotification;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var Contacts = await unitOfWork.Repository<ContactUs>().GetAllAsync();

            return View(Contacts);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contactus = await unitOfWork.Repository<ContactUs>().GetByIdAsync(id);
            unitOfWork.Repository<ContactUs>().Delete(contactus);
            await unitOfWork.Complete();

            _toastNotification.AddSuccessToastMessage("Contact Deleted Successfully");

            return RedirectToAction("index");
        }
    }
}
