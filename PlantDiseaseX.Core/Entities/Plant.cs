using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Core.Entities
{
    public class Plant : BaseEntity
    {
        public  string Name { get; set; }

        public string PictureUrl { get; set; }

        public string Description { get; set; }

        public string season { get; set; }

        public string diseases { get; set; }

        public string treatment { get; set; }

        public string Properties { get; set; }

        public string GeneralUse { get; set; }

        public string MedicalUse { get; set; }

        public string Warnings { get; set; }


       // [ForeignKey("PlantCategory")]
        public int PlantCategoryId { get; set; } // Foreign Key

        // النبات الواحد لديه قسم واحد فقط ولكن القسم لديه اكثر من نبات
        public Plantcategory PlantCategory { get; set; }  // One

        // [ForeignKey("Season")]
        public int PlantSeasonId { get; set; } // Foreign Key
        public Season PlantSeason { get; set; } // One

    }
}
