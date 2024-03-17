//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using PlantDiseaseX.Core;
//using PlantDiseaseX.Core.Services;
//using StackExchange.Redis;

//namespace PlantDiseaseX.Service
//{
//    public class ResponseCacheService : IResponseCacheService
//    {
       


//        private readonly IDatabase _database;
//        public ResponseCacheService(IConnectionMultiplexer redis)
//        {
//            _database  = redis.GetDatabase();
//        }

//        public async Task CacheResponseAsync(string cachekey, string response, TimeSpan timeToLive)
//        {
//            if(response == null )
//               return;


//            var options = new JsonSerializerOptions
//            {
//                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//            };

//            var serilizedResponse = JsonSerializer.Serialize(response, options);

//            await _database.StringSetAsync(cachekey, serilizedResponse, timeToLive);
//        }

//        public Task CacheResponseAsync(string cacheKey, object? value, TimeSpan timeSpan)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<string> GetCacheResponseAsync(string cachekey)
//        {
//            var cachedResponse = await _database.StringGetAsync(cachekey);

//            if (cachedResponse.IsNullOrEmpty) return null;
//            return cachedResponse;
//        }
//    }
//}
