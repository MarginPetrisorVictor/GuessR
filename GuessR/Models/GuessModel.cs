namespace GuessR.Models
{
    public class GuessModel
    {
        public int Id { get; set; }
        public string GuessRiddle { get; set; }
        public string GuessAnswer { get; set; }
        public int Score { get; set; }
        public string QuestionType { get; set; } // Add a new property to store the type of the question
        public string ContentType { get; set; }
        public string ContentUrl { get; set; }
        public GuessModel() { }
    }
}
