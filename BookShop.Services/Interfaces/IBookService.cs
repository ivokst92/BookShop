namespace BookShop.Services.Interfaces
{
    using BookShop.Services.Models;
    using System;
    using System.Collections.Generic;

    public interface IBookService
    {
        BookDetailsServiceModel Details(int id);

        IEnumerable<BookListingServiceModel> All(string searchText);

        int Create(string title,
                   string description,
                   decimal price,
                   int copies,
                   int? edition,
                   int? ageRestriction,
                   DateTime releaseDate,
                   int AuthorId,
                   string categories);

        bool Exists(int id);

        bool Update(int id,
                   string title,
                   string description,
                   decimal price,
                   int copies,
                   int? edition,
                   int? ageRestriction,
                   DateTime releaseDate,
                   int authorId);

        bool Delete(int id);
    }
}
