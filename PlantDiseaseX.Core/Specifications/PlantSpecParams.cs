namespace PlantDiseaseX.Core.Specifications
{
    public class PlantSpecParams
    {
        private const int MaxPageSize = 500;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }


        

        public int PageIndex { get; set; } = 1;

        public string? Sort { get; set; }

        public int? CategoryId { get; set; }

        public int? SeasonId { get; set; }



        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }
    }
}
