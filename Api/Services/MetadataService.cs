using Api.Databases;
using Api.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Api.Services
{
    public class MetadataService
    {
        private readonly Database _database;
        public MetadataService(Database database)
        {
            _database = database;
        }

        public bool Add(MetadataDto metadata)
        {
            var lastId = _database.Metadatas.Max(x => x.Id);
            return _database.Add(metadata.ToModel(lastId + 1));
        }

        public List<MetadataDto> Get(int movieId)
        {
            var result = _database.Metadatas.Where(x => x.IsValid && x.MovieId == movieId)
                 .GroupBy(x => x.Language)
                 .Select(x => x.OrderByDescending(i => i.Id).First())
                 .OrderBy(x => x.Language)
                 .Select(x=> new MetadataDto(x))
                 .ToList();

            return result;
        }
    }
}
