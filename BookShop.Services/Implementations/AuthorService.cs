namespace BookShop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Interfaces;
    using BookShop.Services.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext db;

        public AuthorService(BookShopDbContext db)
        {
            this.db = db;
        }

        public AuthorDetailsServiceModel Details(int id)
        => this.db.Authors
            .Where(x => x.Id == id)
            .ProjectTo<AuthorDetailsServiceModel>()
            .FirstOrDefault();

        public int Create(string firstName, string lastName)
        {
            var author = new Author()
            {
                FirstName = firstName,
                LastName = lastName,
            };

            this.db.Authors.Add(author);
            this.db.SaveChanges();

            return author.Id;
        }

        public IEnumerable<BookWithCategoriesServiceModel> Books(int authorId)
        => this.db.Books
                .Where(b => b.AuthorId == authorId)
                .ProjectTo<BookWithCategoriesServiceModel>()
                .ToList();

        public bool Exists(int authorId)
        => this.db.Authors.Any(x => x.Id == authorId);
    }
}
