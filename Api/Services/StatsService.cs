using Api.Databases;
using Api.Dtos;
using System.Collections.Generic;
using System.Linq;
namespace Api.Services
{
    public class StatsService
    {
        private readonly Database _database;
        public StatsService(Database database)
        {
            _database = database;
        }

        public List<StatsDto> GetAll()
        {
            var result = _database.Stats
                .GroupBy(x => x.movieId)
                .Select(x => new StatsDto
                {
                    MovieId = x.Key,
                    Title = _database.MovieInfos.FirstOrDefault(m => m.Id == x.Key)?.Title,
                    ReleaseYear = _database.MovieInfos.FirstOrDefault(m => m.Id == x.Key)?.ReleaseYear,
                    Watches = x.Count(),
                    AverageWatchDurationS = x.Sum(m => m.watchDurationMs / 1000) / x.Count() 
                })
                .OrderByDescending(x => x.Watches)
                .ThenByDescending(x => x.ReleaseYear)
                .ToList();

            return result;
        }
    }
}
