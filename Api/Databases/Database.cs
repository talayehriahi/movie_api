using Api.Models;
using CsvHelper;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Api.Databases
{
    public class Database
    {
        private bool _isLoading;
        private ConcurrentBag<Metadata> _metadatas = new ConcurrentBag<Metadata>();
        private ConcurrentBag<Stats> _stats = new ConcurrentBag<Stats>();
        private ConcurrentBag<MovieInfo> _movieInfo = new ConcurrentBag<MovieInfo>();

        public List<Metadata> Metadatas {get {return _isLoading ? new List<Metadata>() : _metadatas.ToList();}}
        public List<Stats> Stats {get {return _isLoading ? new List<Stats>() : _stats.ToList();}}
        public List<MovieInfo> MovieInfos { get {return _isLoading ? new List<MovieInfo>() : _movieInfo.ToList();}}
        public Database()
        {
            Load();
        }
        private void Load()
        {
            _isLoading = true;

            LoadMetadata();
            LoadStats();
            LoadMovieInfo();

            _isLoading = false;
        }


        private void LoadMetadata()
        {
            using (var reader = new StreamReader(@"Databases\Metadata.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Metadata>();
                foreach (var metadata in records)
                {
                    _metadatas.Add(metadata);
                }
            }
        }

        private void LoadStats()
        {
            using (var reader = new StreamReader(@"Databases\stats.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Stats>();
                foreach (var record in records)
                {
                    _stats.Add(record);
                }
            }
        }

        private void LoadMovieInfo()
        {
            var data = _metadatas
                .GroupBy(x => x.MovieId)
                .Select(x => new MovieInfo
                {
                    Id = x.Key,
                    Title = x.OrderByDescending(m => m.Id).First(m => m.Language == "EN").Title,
                    ReleaseYear = x.OrderByDescending(m => m.Id).First().ReleaseYear
                }
                )
                .ToList();
            foreach (var info in data)
            {
                _movieInfo.Add(info);
            }

        }

        public bool Add(Metadata metadata)
        {
            if (_isLoading)
                return false;

            _metadatas.Add(metadata);

            return true;
        }
    }
}
