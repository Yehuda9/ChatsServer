namespace Rating_page.Models
{
    public class RatingService : IRatingService
    {
        private static List<RatingClass> RatingList { get; set; } = new List<RatingClass>();

        public void Create(string composer, string title, string description, int rating)
        {
            int id;
            if (RatingList.Count > 0) 
                id = RatingList.Max(x => x.Id) + 1;
            else
                id = 0;
            RatingList.Add(new RatingClass() { 

                Composer = composer,
                Id = id,
                Title = title,
                Description = description,
                Rating = rating,
                DateTime = DateTime.Now

            });
        }

        public List<RatingClass> GetAll()
        {
            return RatingList;
        }

        public RatingClass? Get(int id)
        {
            return RatingList.Find(x => x.Id == id);
        }

        public void Delete (int id)
        {
            RatingClass? rating = Get(id);
            if (rating != null) RatingList.Remove(rating);
        }

        public void Edit(int Id, string composer, string title, string description, int rating)
        {
            var ratingObject = Get(Id);
            if (ratingObject != null)
            {
                ratingObject.Composer = composer;
                ratingObject.Title = title;
                ratingObject.Description = description;
                ratingObject.Rating = rating;
            }
        }

        public List<RatingClass> SearchRating(string text)
        {
            List<RatingClass> results = GetAll().Where(
                x => x.Title.Contains(text) || x.Composer.Contains(text) || x.Description.Contains(text)
                ).ToList();
            return results;
        }


    }
}

