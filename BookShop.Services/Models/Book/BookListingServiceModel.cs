namespace BookShop.Services.Models
{
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;

    public class BookListingServiceModel : IMapFrom<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
