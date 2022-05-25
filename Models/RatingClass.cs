using System.ComponentModel.DataAnnotations;

namespace Rating_page.Models
{
    public class RatingClass
    {
        public int Id { get; set; }

        [Display(Name = "Created By")]
        [Required(ErrorMessage = "Please insert your name.")]
        public string Composer { get; set; }
        
        [MaxLength(50, ErrorMessage = "Title max legnth is 50 characters")]
        public string Title { get; set; }

        [MaxLength(300, ErrorMessage = "Description max legnth is 300 characters")]
        public string Description { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 to 5.")]
        public int Rating { get; set; }

        [Display(Name = "Created At")]
        public DateTime DateTime { get; set; }
        
    }
}
