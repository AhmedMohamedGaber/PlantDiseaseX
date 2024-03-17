namespace PlantDiseaseX.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request, You Have Made",
                401 => "Authorized, You Are Not",
                404 => "Resource was not found",
                500 => "The server has encountered an unexpected condition or configuration problem that prevents it from fulfilling the request made by the browser or client",
                _ => null
            };
        }
    }
}
