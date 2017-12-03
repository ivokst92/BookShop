namespace BookShop.Services.Interfaces
{
    using BookShop.Services.Models.Category;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        IEnumerable<CategoryServiceModel> All();

        CategoryServiceModel ById(int id);

        bool Exists(int id);

        bool Duplicate(string name);

        bool Update(int id, string name);

        int Create(string name);

        bool Delete(int id);
    }
}
