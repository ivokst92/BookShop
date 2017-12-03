namespace BookShop.Api.Models.Books
{
    using BookShop.Data;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BookRequestModel
    {
        [Required]
        [MinLength(DataConstants.BookTitleMinLength)]
        [MaxLength(DataConstants.BookTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Copies { get; set; }

        public int? AgeRestriction { get; set; }
        
        public DateTime ReleaseDate { get; set; }

        public int? Edition { get; set; }

        public int AuthorId { get; set; }
    }
}
