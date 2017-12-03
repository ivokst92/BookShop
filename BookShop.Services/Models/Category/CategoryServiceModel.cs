namespace BookShop.Services.Models.Category
{
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;

    public class CategoryServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
