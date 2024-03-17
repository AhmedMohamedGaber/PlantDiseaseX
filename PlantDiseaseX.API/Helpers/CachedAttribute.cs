//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using PlantDiseaseX.Core.Services;
//using System;
//using System.Text;

//namespace PlantDiseaseX.API.Helpers
//{
//    public class CachedAttribute : Attribute, IAsyncActionFilter
//    {
//        private readonly int _timeToLiveSeconds;

//        public CachedAttribute(int timeToLiveSeconds)
//        {
//            _timeToLiveSeconds = timeToLiveSeconds;
//        }
//        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//        {
//           var cacheServcie =  context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();


//            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

//            var cachedResponse = await cacheServcie.GetCacheResponseAsync(cacheKey);

//            if (!string.IsNullOrEmpty(cachedResponse))
//            {
//                var contentResult = new ContentResult()
//                {
//                    Content = cachedResponse,
//                    ContentType="application/json",
//                    StatusCode = 200,
                    
                    
//                };
//                context.Result = contentResult;
//                return;
//            }


//            var executedEndpointContext =  await next();

//            if (executedEndpointContext.Result is OkObjectResult okObjectResults) 
//            {
//                await cacheServcie.CacheResponseAsync(cacheKey, okObjectResults.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
//            }
//        }

//        private string GenerateCacheKeyFromRequest(HttpRequest request)
//        {
//            // {{URL}} / api/Plantas?pageIndex=1&pageSize=5&sort=name

//            var KeyBulider = new StringBuilder();

//            KeyBulider.Append(request.Path); // /api/Plants

//            // pageIndex=1
//            //pageSize=5
//            //sort=name


//            // /api/Plants
//            foreach (var (key,value) in request.Query)
//            {
//                KeyBulider.Append($"|{key}-{value}");
//                // /api/Plants|pageIndex-1
//                // /api/Plants|pageIndex-1 | pageSize-5
//                // /api/Plants|pageIndex-1 | pageSize-5 | sort-name
//            }

//            return KeyBulider.ToString();
//        }
//    }
//}
