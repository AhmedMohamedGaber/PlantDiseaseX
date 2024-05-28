using Admin.PlantDiseaseX.Helper;
using Admin.PlantDiseaseX.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PlantDiseaseX.Core.Repositories;
using PlantDiseaseX.Repository.Data;
using System.Collections.Generic;
using PlantDiseaseX.API.Dtos;
using NToastNotify;
using Microsoft.AspNetCore.Authorization;


namespace Admin.PlantDiseaseX.Controllers
{
    [Authorize]

    public class NewsArticleController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public NewsArticleController(IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var newsarticle = await _unitOfWork.Repository<NewsArticle>().GetAllAsync();
            // var mappednewsarticle = _mapper.Map<List<NewsArticleViewModel>>(newsarticle);

            var mappednewsarticle = _mapper.Map<IReadOnlyList<NewsArticle>, IReadOnlyList<NewsArticleViewModel>>(newsarticle);

            return View(mappednewsarticle);
        }



        public IActionResult Create()
        {

            return View();
        }




        [HttpPost]

        public async Task<IActionResult> Create(NewsArticleViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ModelState.IsValid)
            {


                if (model.NewsImage != null)
                {
                    model.NewsPicture = NewsPictureSettings.UploadFile(model.NewsImage, "newsarticle");
                }
                else { 
                    model.NewsPicture = "images/newsarticle/DefaultNewsArticle.jpg";
                }


                var mappedNewsArticle = _mapper.Map<NewsArticleViewModel,NewsArticle>(model);

                await _unitOfWork.Repository<NewsArticle>().AddAsync(mappedNewsArticle);

                await _unitOfWork.Complete();

               
                _toastNotification.AddSuccessToastMessage("NewsArticle Added Successfully");
                return RedirectToAction("Index");
            }

            // If ModelState.IsValid is false, ensure that PictureUrl is initialized
            if (model.NewsPicture == null)
            {
                model.NewsPicture= "images/newsarticle/DefaultNewsArticle.jpg";
            }


            return View(model);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var newsarticle = await _unitOfWork.Repository<NewsArticle>().GetByIdAsync(id);
            var mappedNewsArticle = _mapper.Map<NewsArticle, NewsArticleViewModel>(newsarticle);
            return View("Edit", mappedNewsArticle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewsArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                if (model.NewsImage != null)
                {
                    if (model.NewsPicture != null)
                    {
                        NewsPictureSettings.DeleteFile(model.NewsPicture, "newsarticle");
                        model.NewsPicture = NewsPictureSettings.UploadFile(model.NewsImage, "newsarticle");
                    }
                    else
                    {
                        model.NewsPicture = NewsPictureSettings.UploadFile(model.NewsImage, "newsarticle");

                    }

                }
                var mappedNewsArticle = _mapper.Map<NewsArticleViewModel, NewsArticle>(model);
                _unitOfWork.Repository<NewsArticle>().Update(mappedNewsArticle);
                var result = await _unitOfWork.Complete();
                if (result > 0)
                {
                    _toastNotification.AddSuccessToastMessage("NewsArticle Edited Successfully");

                    return RedirectToAction("Index");
                }
            }
            return View("Edit", model);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var newsarticle = await _unitOfWork.Repository<NewsArticle>().GetByIdAsync(id);
            var mappednewsarticle = _mapper.Map<NewsArticle, NewsArticleViewModel>(newsarticle);
            return View(mappednewsarticle);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, NewsArticleViewModel model)
        {
            if (id != model.Id)
                return NotFound();
            try
            {
                var newsarticle = await _unitOfWork.Repository<NewsArticle>().GetByIdAsync(id);
                if (newsarticle.NewsPicture != null)
                    NewsPictureSettings.DeleteFile(newsarticle.NewsPicture, "newsarticle");

                _unitOfWork.Repository<NewsArticle>().Delete(newsarticle);
                await _unitOfWork.Complete();
                _toastNotification.AddErrorToastMessage("NewsArticle Deleted Successfully");
                return RedirectToAction("Index");

            }
            catch (System.Exception)
            {

                return View(model);
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            var newsarticle = await _unitOfWork.Repository<NewsArticle>().GetByIdAsync(id);
            var mappedNewsArticle = _mapper.Map<NewsArticle, NewsArticleViewModel>(newsarticle);
            return View(mappedNewsArticle);
        }


    }
}

