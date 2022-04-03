namespace Api.Dtos
{
    public class StatsDto
    {
        public int MovieId { set; get; }
        public string Title { set; get; }
        public int? ReleaseYear { set; get; }
        public int Watches { set; get; }
        public int AverageWatchDurationS { set; get; }
    }
}
