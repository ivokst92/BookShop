namespace BookShop.Services.Interfaces
{
    using BookShop.Services.Models;
    using System.Collections.Generic;
    
    public interface IAuthorService
    {
        AuthorDetailsServiceModel Details(int id);

        int Create(string firstName, string lastName);

        IEnumerable<BookWithCategoriesServiceModel> Books(int authorId);

        bool Exists(int authorId);
    }
}
