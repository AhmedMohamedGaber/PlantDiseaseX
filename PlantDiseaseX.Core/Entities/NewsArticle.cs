using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;




namespace PlantDiseaseX.Core.Entities
{



    public class NewsArticle : BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string NewsPicture {  get; set; }

        public DateTime Date { get; set; }
       // public string FullNewsPictureUrl => !string.IsNullOrEmpty(NewsPicture) ? $"http://plantdiseasexapi.runasp.net/{NewsPicture}" : null;
    }
}
