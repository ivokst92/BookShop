namespace BookShop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Common.Extensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Interfaces;
    using BookShop.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly BookShopDbContext db;
        public BookService(BookShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<BookListingServiceModel> All(string searchText)
        {
            if (searchText == null)
                searchText = string.Empty;

            return this.db.Books
            .Where(x => x.Title.ToLower().Contains(searchText.ToLower()))
            .OrderBy(x => x.Title)
            .Take(10)
            .ProjectTo<BookListingServiceModel>()
            .ToList();
        }

        public int Create(string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories)
        {
            var categoryNames = categories
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();

            var existingCategories = this.db
                .Categories
                .Where(x => categoryNames.Contains(x.Name))
                .ToList();

            var allCategories = new List<Category>(existingCategories);

            foreach (var categoryName in categoryNames)
            {
                if (existingCategories.All(c => c.Name != categoryName))
                {
                    var category = new Category
                    {
                        Name = categoryName
                    };
                    this.db.Categories.Add(category);

                    allCategories.Add(category);
                }
            }
            this.db.SaveChanges();

            var book = new Book
            {
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                Edition = edition,
                AgeRestriction = ageRestriction,
                ReleaseDate = releaseDate,
                AuthorId = authorId,
            };

            allCategories.ForEach(c => book.Categories.Add(new BookCategory
            {
                CategoryId = c.Id
            }));

            this.db.Add(book);
            this.db.SaveChanges();

            return book.Id;
        }

        public bool Delete(int id)
        {
            var book = this.db.Books.Find(id);

            if (book == null)
            {
                return false;
            }

            this.db.Books.Remove(book);
            this.db.SaveChanges();
            return true;
        }

        public BookDetailsServiceModel Details(int id)
        => this.db.Books
            .Where(x => x.Id == id)
            .ProjectTo<BookDetailsServiceModel>()
            .FirstOrDefault();

        public bool Exists(int id)
            => this.db.Books.Any(b => b.Id == id);

        public bool Update(int id, string title, string description, decimal price, int copies, int? edition, int? ageRestriction, DateTime releaseDate, int authorId)
        {
            var book = this.db.Books.Find(id);

            if (book == null)
            {
                return false;
            }

            book.Title = title;
            book.Description = description;
            book.Price = price;
            book.Copies = copies;
            book.Edition = edition;
            book.AgeRestriction = ageRestriction;
            book.ReleaseDate = releaseDate;
            book.AuthorId = authorId;

            this.db.Books.Update(book);
            this.db.SaveChanges();
            return true;
        }
    }
}
