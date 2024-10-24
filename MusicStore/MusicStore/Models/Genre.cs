namespace MusicStore.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public ICollection<Album> Album { get; set; } = new List<Album>();
    }
}
