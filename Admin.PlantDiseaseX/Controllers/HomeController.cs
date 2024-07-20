using Admin.PlantDiseaseX.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.Core;
using PlantDiseaseX.Repository;
using System.Diagnostics;
using static StackExchange.Redis.Role;
using Admin.PlantDiseaseX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PlantDiseaseX.Core.Repositories;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using PlantDiseaseX.Core.Entities;
using plantdiseasex.core.entities;
using PlantDiseaseX.Core.Repositories;
using Admin.PlantDiseaseX.Repository;
using Admin.PlantDiseaseX.Services; // Adjust namespace as per your project structure


namespace Admin.PlantDiseaseX.Controllers
{
    [Authorize]
    
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IMapper mapper, UserRepository userRepository, IUserService userService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var plants = await _unitOfWork.Repository<Plant>().GetAllAsync();
            var mappedPlants = _mapper.Map<IReadOnlyList<Plant>, IReadOnlyList<PlantViewModel>>(plants);

            var categories = await _unitOfWork.Repository<Plantcategory>().GetAllAsync();
            var mappedCategories = _mapper.Map<IReadOnlyList<Plantcategory>,IReadOnlyList<CategoryViewModel>>(categories);
           
            
            var seasons = await _unitOfWork.Repository<Season>().GetAllAsync();
            var mappedSeasons = _mapper.Map<IReadOnlyList<Season>, IReadOnlyList<SeasonViewModel>>(seasons);


            var NewsArticles = await _unitOfWork.Repository<NewsArticle>().GetAllAsync();
            var mappedNewsArticles = _mapper.Map<IReadOnlyList<NewsArticle>, IReadOnlyList<NewsArticleViewModel>>(NewsArticles);

            var CornDiseases = await _unitOfWork.Repository<corndisease>().GetAllAsync();
            var mappedCornDiseases = _mapper.Map<IReadOnlyList<corndisease>, IReadOnlyList<CornDiseasecsViewModel>>(CornDiseases);


            var messages = await _unitOfWork.Repository<ContactUs>().GetAllAsync();
            var mappedMessages = _mapper.Map<IReadOnlyList<ContactUs>, IReadOnlyList<ContactUsViewModel>>(messages);


            var signedInUsersCount = await _userRepository.GetSignedInUsersCountAsync();
            ViewBag.SignedInUsersCount = signedInUsersCount;


            var loggedInUsers = await _userService.GetCurrentlyLoggedInUsersAsync();
            ViewBag.LoggedInUsers = loggedInUsers;

            var viewModel = new HomeIndexViewModel
            {
                Plants = mappedPlants,
                Categories = mappedCategories,
                Seasons = mappedSeasons,
                NewsArticles = mappedNewsArticles,
                CornDiseases = mappedCornDiseases,
                Messages = mappedMessages

            };

            return View(viewModel);


           // return View(mappedPlants,mappedNewsArticles,mappedCornDiseases);
        }
      
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
