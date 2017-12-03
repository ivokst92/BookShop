namespace BookShop.Api.Models
{
    using BookShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class CategoryRequestModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
