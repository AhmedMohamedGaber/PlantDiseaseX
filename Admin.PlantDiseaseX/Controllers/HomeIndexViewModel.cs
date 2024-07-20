using Admin.PlantDiseaseX.Models;

namespace Admin.PlantDiseaseX.Controllers
{
    public class HomeIndexViewModel
    {
        public IReadOnlyList<PlantViewModel> Plants { get; set; }

        public IReadOnlyList<CategoryViewModel> Categories { get; set; }
        public IReadOnlyList<SeasonViewModel> Seasons { get; set; }

        public IReadOnlyList<NewsArticleViewModel> NewsArticles { get; set; }
        public IReadOnlyList<CornDiseasecsViewModel> CornDiseases { get; set; }


        public IReadOnlyList<ContactUsViewModel> Messages { get; set; }


        public List<ApplicationUser> LoggedInUsers { get; set; }
    }
}
