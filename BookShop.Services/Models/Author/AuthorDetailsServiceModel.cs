namespace BookShop.Services.Models
{
    using AutoMapper;
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorDetailsServiceModel : IMapFrom<Author>, ICustomMapping
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<string> Books { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Author, AuthorDetailsServiceModel>()
                 .ForMember(a => a.Books, cfg => cfg.MapFrom(a => a.Books.Select(c => c.Title)));
        }
    }
}
