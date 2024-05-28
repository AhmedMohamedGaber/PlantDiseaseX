namespace PlantDiseaseX.API.Dtos
{
    public class NewToReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NewsPicture { get; set; }
        public DateTime Date { get; set; }

     //   public string FullNewsPictureUrl => !string.IsNullOrEmpty(NewsPicture) ? $"http://plantdiseasexapi.runasp.net/{NewsPicture}" : null;
    }
}
