using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace GuessR.Models
{
    public class GuessModel
    {
        public int Id { get; set; }
        public string GuessRiddle { get; set; }
        public string GuessAnswer { get; set; }
        public int Score { get; set; }
        public string Question { get; set; }
        public string QuestionType { get; set; } = ""; // Add a new property to store the type of the question
        public string ContentType { get; set; } = "";
        public string ContentUrl { get; set; } = "";

        [Display(Name = "Profile Picture")]
        [NotMapped]
        public IFormFile? ProfilePicture { get; set; }

        public GuessModel() { }
    }
}
