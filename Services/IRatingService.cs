namespace Rating_page.Models
{
    public interface IRatingService
    {
        public void Create(string composer, string title, string description, int rating);
        public void Edit(int Id, string composer, string title, string description, int rating);
        public RatingClass? Get(int id);
        public List<RatingClass> GetAll();
        public void Delete(int id);

        public List<RatingClass> SearchRating(string text);

    }
}
