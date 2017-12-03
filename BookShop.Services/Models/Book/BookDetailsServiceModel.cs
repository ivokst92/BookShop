namespace BookShop.Services.Models
{
    using AutoMapper;
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;
    using System.Linq;

    public class BookDetailsServiceModel : BookWithCategoriesServiceModel, IMapFrom<Book>, ICustomMapping
    {
        public string Author { get; set; }

        public override void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(b => b.Categories, cfg => cfg
                   .MapFrom(b => b.Categories.Select(c => c.Category.Name)))
               .ForMember(b => b.Author, cfg => cfg
                   .MapFrom(b => b.Author.FirstName + " " + b.Author.LastName));
        }
    }
}
