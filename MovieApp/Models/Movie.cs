using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public partial class Movie
    {
        public int MovieId { set; get; }
        [Required(ErrorMessage = "Enter Movie Name")]
        public string MovieName { set; get; }
        [Required(ErrorMessage = "Give A rating To Movie")]
        [Range(0, 10)]
        public int Rating { set; get; }
    }
}
