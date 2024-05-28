using Admin.PlantDiseaseX.Helper;
using Admin.PlantDiseaseX.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core;
using plantdiseasex.core.entities;
using Microsoft.AspNetCore.Authorization;

namespace Admin.PlantDiseaseX.Controllers
{
    [Authorize]


    public class CornDiseaseController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CornDiseaseController(IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var corndiseas = await _unitOfWork.Repository<corndisease>().GetAllAsync();
            var mappedCornDisease = _mapper.Map<List<CornDiseasecsViewModel>>(corndiseas);


            return View(mappedCornDisease);
        }
       


        public IActionResult Create()
        {

            return View();
        }




        [HttpPost]

        public async Task<IActionResult> Create(CornDiseasecsViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ModelState.IsValid)
            {


                if (model.CorndiseaseImage1 != null && model.CorndiseaseImage2 != null && model.CorndiseaseImage3 != null)
                {
                    model.corndiseasepicture1 = CornDiseasePictureSettings.UploadFile(model.CorndiseaseImage1, "corndisease");
                    model.corndiseasepicture2 = CornDiseasePictureSettings.UploadFile(model.CorndiseaseImage2, "corndisease");
                    model.corndiseasepicture3 = CornDiseasePictureSettings.UploadFile(model.CorndiseaseImage3, "corndisease");
                }
                else
                {
                    model.corndiseasepicture1 = "images/corndisease/DefaultNewsArticle1.jpg";
                    model.corndiseasepicture2 = "images/corndisease/DefaultNewsArticle2.jpg";
                    model.corndiseasepicture3 = "images/corndisease/DefaultNewsArticle3.jpg"; 
                }

                var mappedCornDisease = _mapper.Map<CornDiseasecsViewModel, corndisease>(model);

                await _unitOfWork.Repository<corndisease>().AddAsync(mappedCornDisease);

                await _unitOfWork.Complete();

                // Setting the cookie
                var options = new CookieOptions
                {
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };
                Response.Cookies.Append("MyCookie", "cookieValue", options);


                _toastNotification.AddSuccessToastMessage("CornDisease Added Successfully");
                return RedirectToAction("Index");
            }

            // If ModelState.IsValid is false, ensure that PictureUrl is initialized



            return View(model);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var corndisease = await _unitOfWork.Repository<corndisease>().GetByIdAsync(id);
            var mappedCornDisease = _mapper.Map<corndisease,CornDiseasecsViewModel>(corndisease);
            return View("Edit", mappedCornDisease);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CornDiseasecsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                if (model.CorndiseaseImage1 != null && model.CorndiseaseImage2 != null && model.CorndiseaseImage3 != null)
                {
                    if (model.corndiseasepicture1 != null && model.corndiseasepicture2 != null && model.corndiseasepicture3 != null)
                    {
                        CornDiseasePictureSettings.DeleteFile(model.corndiseasepicture1, "corndisease");
                        model.corndiseasepicture1 = CornDiseasePictureSettings.UploadFile(model.CorndiseaseImage1, "corndisease");

                        CornDiseasePictureSettings.DeleteFile(model.corndiseasepicture2, "corndisease");
                        model.corndiseasepicture2 = CornDiseasePictureSettings.UploadFile(model.CorndiseaseImage2, "corndisease");

                        CornDiseasePictureSettings.DeleteFile(model.corndiseasepicture3, "corndisease");
                        model.corndiseasepicture3 = CornDiseasePictureSettings.UploadFile(model.CorndiseaseImage3, "corndisease");

                    }
                    else
                    {
                        model.corndiseasepicture1 = CornDiseasePictureSettings.UploadFile(model.CorndiseaseImage1, "corndisease");
                        model.corndiseasepicture2 = CornDiseasePictureSettings.UploadFile(model.CorndiseaseImage2, "corndisease");
                        model.corndiseasepicture3 = CornDiseasePictureSettings.UploadFile(model.CorndiseaseImage3, "corndisease");


                    }

                }
                var mappedCornDisease = _mapper.Map<CornDiseasecsViewModel, corndisease>(model);
                _unitOfWork.Repository<corndisease>().Update(mappedCornDisease);
                var result = await _unitOfWork.Complete();
                if (result > 0)
                {
                    _toastNotification.AddSuccessToastMessage("CornDisease Edited Successfully");

                    return RedirectToAction("Index");
                }
            }
            return View("Edit", model);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var corndisease = await _unitOfWork.Repository<corndisease>().GetByIdAsync(id);
            var mappedcorndiseae = _mapper.Map<corndisease,CornDiseasecsViewModel>(corndisease);
            return View(mappedcorndiseae);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CornDiseasecsViewModel model)
        {
            if (id != model.Id)
                return NotFound();
            try
            {
                var corndisease = await _unitOfWork.Repository<corndisease>().GetByIdAsync(id);
                if (model.corndiseasepicture1 != null && model.corndiseasepicture2 != null && model.corndiseasepicture3 != null)
                {
                    CornDiseasePictureSettings.DeleteFile(model.corndiseasepicture1, "corndisease");
                    CornDiseasePictureSettings.DeleteFile(model.corndiseasepicture2, "corndisease");
                    CornDiseasePictureSettings.DeleteFile(model.corndiseasepicture3, "corndisease");
                }

                _unitOfWork.Repository<corndisease>().Delete(corndisease);
                await _unitOfWork.Complete();
                _toastNotification.AddErrorToastMessage("CornDisease Deleted Successfully");
                return RedirectToAction("Index");

            }
            catch (System.Exception)
            {

                return View(model);
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            var corndisease = await _unitOfWork.Repository<corndisease>().GetByIdAsync(id);
            var mappedCornDisease = _mapper.Map<corndisease, CornDiseasecsViewModel>(corndisease);
            return View(mappedCornDisease);
        }


    }
}
