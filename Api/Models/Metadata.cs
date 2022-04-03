namespace Api.Models
{
    public class Metadata
    {
        public int Id { set; get; }
        public int MovieId { set; get; }
        public string Title { set; get; }
        public string Language { set; get; }
        public string Duration { set; get; }
        public int ReleaseYear { set; get; }

        public bool IsValid => 
            Id > 0 && MovieId > 0 
            && !string.IsNullOrEmpty(Title) 
            && !string.IsNullOrEmpty(Language)
            && !string.IsNullOrEmpty(Duration) 
            && ReleaseYear > 0;
    }
}
