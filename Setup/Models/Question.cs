using Microsoft.AspNetCore.Mvc;

namespace Setup.Models
{
    public class Question
    {
        public string Content { get; set; }
        public Question(string content)
        {
            Content = content;
        }
    }
}
