using Admin.PlantDiseaseX.Helper;
using Admin.PlantDiseaseX.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PlantDiseaseX.Core;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Repositories;
using PlantDiseaseX.Repository.Data;
using System.Collections.Generic;
using PlantDiseaseX.API.Dtos;
using NToastNotify;
using PlantDiseaseX.Core.Specifications;
using Microsoft.AspNetCore.Authorization;


namespace Admin.PlantDiseaseX.Controllers
{
    [Authorize]

    public class PlantController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public PlantController(IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var plants = await _unitOfWork.Repository<Plant>().GetAllAsync();
                var mappedplants = _mapper.Map<IReadOnlyList<Plant>, IReadOnlyList<PlantViewModel>>(plants);

                return View(mappedplants);
            }
            else
            {
                var specParams = new PlantSpecParams { Search = SearchValue};
                var spec = new PlantWithCategoryAndSeasonSpecification(specParams);
                var plants = await _unitOfWork.Repository<Plant>().GetAllWithSpecAsync(spec);
                var mappedplants = _mapper.Map<IReadOnlyList<Plant>, IReadOnlyList<PlantViewModel>>(plants);

                return View(mappedplants);
            }
        }


        //public async Task<IActionResult> Index()
        //{
        //    var plants = await _unitOfWork.Repository<Plant>().GetAllAsync();
        //    var mappedplants = _mapper.Map<IReadOnlyList<Plant>, IReadOnlyList<PlantViewModel>>(plants);

        //    return View(mappedplants);
        //}



        public IActionResult Create()
        {

            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(PlantViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    if (model.Image != null)
        //    {
        //        model.PictureUrl = PictureSettings.UploadFile(model.Image);
        //    }
        //    else
        //    {
        //        model.PictureUrl = "https://plantdiseasexdashbord.runasp.net/images/plants/DefaultImage.jpg";
        //    }

        //    var mappedPlant = _mapper.Map<PlantViewModel, Plant>(model);
        //    await _unitOfWork.Repository<Plant>().AddAsync(mappedPlant);
        //    await _unitOfWork.Complete();

        //    _toastNotification.AddSuccessToastMessage("Plant Created Successfully");
        //    return RedirectToAction("Index");
        //}





        [HttpPost]

        public async Task<IActionResult> Create(PlantViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ModelState.IsValid)
            {


                if (model.Image != null)
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "plants");
                else
                    model.PictureUrl = "images/plants/DefaultImage.jpg";

                var mappedPlant = _mapper.Map<PlantViewModel, Plant>(model);
                await _unitOfWork.Repository<Plant>().AddAsync(mappedPlant);

                await _unitOfWork.Complete();


                _toastNotification.AddSuccessToastMessage("Plant Created Successfully");

                return RedirectToAction("Index");
            }



            // If ModelState.IsValid is false, ensure that PictureUrl is initialized
            if (model.PictureUrl == null)
            {
                model.PictureUrl = "images/plants/DefaultImage.png";
            }


            return View(model);

        }



        public async Task<IActionResult> Edit(int id)
        {
            var plant = await _unitOfWork.Repository<Plant>().GetByIdAsync(id);
            var mappedPlant = _mapper.Map<Plant, PlantViewModel>(plant);
            return View("Edit", mappedPlant);
        }



        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, PlantViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    if (id != model.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (model.Image != null)
        //    {
        //        if (!string.IsNullOrEmpty(model.PictureUrl))
        //        {
        //            var fileName = Path.GetFileName(model.PictureUrl);
        //            PictureSettings.DeleteFile(fileName);
        //        }
        //        model.PictureUrl = PictureSettings.UploadFile(model.Image);
        //    }

        //    var mappedPlant = _mapper.Map<PlantViewModel, Plant>(model);
        //    _unitOfWork.Repository<Plant>().Update(mappedPlant);
        //    await _unitOfWork.Complete();

        //    _toastNotification.AddSuccessToastMessage("Plant Updated Successfully");
        //    return RedirectToAction("Index");
        //}








        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    if (model.PictureUrl != null)
                    {
                        PictureSettings.DeleteFile(model.PictureUrl, "plants");
                        model.PictureUrl = PictureSettings.UploadFile(model.Image, "plants");
                    }
                    else
                    {
                        model.PictureUrl = PictureSettings.UploadFile(model.Image, "plants");

                    }

                }
                var mappedPlant = _mapper.Map<PlantViewModel, Plant>(model);
                _unitOfWork.Repository<Plant>().Update(mappedPlant);
                var result = await _unitOfWork.Complete();
                if (result > 0)
                {
                    _toastNotification.AddSuccessToastMessage("Plant Updated Successfully");

                    return RedirectToAction("Index");
                }
            }
            return View("Edit", model);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var plant = await _unitOfWork.Repository<Plant>().GetByIdAsync(id);
            var mappedPlant = _mapper.Map<Plant, PlantViewModel>(plant);
            return View(mappedPlant);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id, PlantViewModel model)
        {
            if (id != model.Id)
                return NotFound();
            try
            {
                var plant = await _unitOfWork.Repository<Plant>().GetByIdAsync(id);
                if (plant.PictureUrl != null)
                    PictureSettings.DeleteFile(plant.PictureUrl, "plants");



                _unitOfWork.Repository<Plant>().Delete(plant);
                await _unitOfWork.Complete();

              
                _toastNotification.AddErrorToastMessage("Plant Deleted Successfully");

                // return Ok();
                return RedirectToAction("Index");

            }
            catch (System.Exception)
            {

                return View(model);
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            var plant = await _unitOfWork.Repository<Plant>().GetByIdAsync(id);
            var mappedPlant = _mapper.Map<Plant, PlantViewModel>(plant);
            return View(mappedPlant);
        }


    }
}





//namespace Admin.PlantDiseaseX.Controllers
//{
//    public class PlantController : Controller
//    {
//        private readonly PlantContext _plantcontext;
//        private readonly IWebHostEnvironment webHostEnvironment;

//        public PlantController(PlantContext plantContext)
//        {
//            _plantcontext = plantContext;
//        }
//        public async Task<IActionResult> Index()
//        {

//            var plants = await _plantcontext.Plants.ToListAsync();
//            return View(plants);
//        }

//        public async Task<IActionResult> Create()
//        {

//            var viewModel = new PlantViewModel
//            {
//                PlantCategory = await _plantcontext.PlantCategories.OrderBy(m => m.Name).ToListAsync(),
//                PlantSeason = await _plantcontext.PlantSeasons.OrderBy(m => m.Name).ToListAsync()

//            };
//            return View(viewModel);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(PlantViewModel model)
//        {

//          if (!ModelState.IsValid)
//            {
//                model.PlantCategory = await _plantcontext.PlantCategories.OrderBy(m => m.Name).ToListAsync();
//                model.PlantSeason = await _plantcontext.PlantSeasons.OrderBy(m => m.Name).ToListAsync();
//                return View(model);
//            }

//            var files = Request.Form.Files;
//            if (!files.Any())
//            {
//                model.PlantCategory = await _plantcontext.PlantCategories.OrderBy(m => m.Name).ToListAsync();
//                model.PlantSeason = await _plantcontext.PlantSeasons.OrderBy(m => m.Name).ToListAsync();

//                ModelState.AddModelError("Image", "Please select Plant image!");
//                return View(model);
//            }

//            var image = files.FirstOrDefault();
//            var allowedExtenstion =new List<string>() { ".jpg", ".jpeg", ".png", ".ico", ".icon", ".gif", ".svg" };

//            if (!allowedExtenstion.Contains(Path.GetExtension(image.FileName).ToLower()))
//            {
//                model.PlantCategory = await _plantcontext.PlantCategories.OrderBy(m => m.Name).ToListAsync();
//                model.PlantSeason = await _plantcontext.PlantSeasons.OrderBy(m => m.Name).ToListAsync();
//                ModelState.AddModelError("Image", "Invalid extention only valid extentions");

//                return View(model);
//            }

//            if(image.Length > 1048576)
//            {
//                model.PlantCategory = await _plantcontext.PlantCategories.OrderBy(m => m.Name).ToListAsync();
//                model.PlantSeason = await _plantcontext.PlantSeasons.OrderBy(m => m.Name).ToListAsync();
//                ModelState.AddModelError("Image", "Image cannot be more than 1 MB!");

//                return View(model);
//            }
//            using var dataStream = new MemoryStream();
//            var ImageName = UploadedFile(model.Image);
//            await image.CopyToAsync(dataStream);

//            var plants = new Plant
//            {

//                Name= model.Name,
//                PlantCategoryId=model.PlantCategoryId,
//                PlantSeasonId=model.PlantSeasonId,
//                Description= model.Description,
//                season=model.season,
//                diseases=model.diseases,
//                treatment=model.treatment,
//                Properties=model.Properties,
//                GeneralUse=model.GeneralUse,
//                MedicalUse=model.MedicalUse,
//                Warnings=model.Warnings,
//                PictureUrl =ImageName,
//            };

//            _plantcontext.Plants.Add(plants);
//            _plantcontext.SaveChanges();

//            return RedirectToAction(nameof(Index));
//        }


//        private string UploadedFile(IFormFile file)
//        {
//            string uniqueFileName = null;

//            if (file != null)
//            {
//                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
//                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
//                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
//                using (var fileStream = new FileStream(filePath, FileMode.Create))
//                {
//                    file.CopyTo(fileStream);
//                }
//            }
//            return uniqueFileName;
//        }
//    }
//}
