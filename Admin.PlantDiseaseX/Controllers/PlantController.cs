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





        public IActionResult Create()
        {

            return View();
        }

       



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




