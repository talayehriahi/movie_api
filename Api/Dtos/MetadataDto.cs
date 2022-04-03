using Api.Models;

namespace Api.Dtos
{
    public class MetadataDto
    {
        public int MovieId { set; get; }
        public string Title { set; get; }
        public string Language { set; get; }
        public string Duration { set; get; }
        public int ReleaseYear { set; get; }

        public MetadataDto() { }
        public MetadataDto(Metadata metadata)
        {
            MovieId = metadata.MovieId;
            Title = metadata.Title;
            Language = metadata.Language;
            Duration = metadata.Duration;
            ReleaseYear = metadata.ReleaseYear;
        }

         public Metadata ToModel(int id)
        {
            var result = new Metadata
            {
                Id = id,
                MovieId = MovieId,
                Duration = Duration,
                Language = Language,
                ReleaseYear = ReleaseYear,
                Title = Title
            };

            return result;
        }
    }
}
