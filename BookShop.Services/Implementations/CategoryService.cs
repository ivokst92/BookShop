namespace BookShop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Interfaces;
    using BookShop.Services.Models.Category;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly BookShopDbContext db;

        public CategoryService(BookShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CategoryServiceModel> All()
        => this.db
            .Categories
            .OrderBy(x => x.Id)
            .ProjectTo<CategoryServiceModel>()
            .ToList();

        public CategoryServiceModel ById(int id)
        => this.db
            .Categories
            .Where(x => x.Id == id)
            .ProjectTo<CategoryServiceModel>()
            .FirstOrDefault();

        public int Create(string name)
        {
            var category = new Category
            {
                Name = name
            };

            this.db.Add(category);
            this.db.SaveChanges();

            return category.Id;
        }

        public bool Delete(int id)
        {
            var category = this.db.Categories.Find(id);

            if (category == null)
            {
                return false;
            }

            this.db.Categories.Remove(category);
            this.db.SaveChanges();
            return true;
        }

        public bool Duplicate(string name)
        => this.db.Categories
                 .Any(x => x.Name == name);

        public bool Exists(int id)
        => this.db.Categories
            .Any(x => x.Id == id);

        public bool Update(int id, string name)
        {
            var category = this.db.Categories.Find(id);

            if (category == null)
            {
                return false;
            }

            category.Name = name;
            this.db.Categories.Update(category);
            this.db.SaveChanges();
            return true;
        }
    }
}
