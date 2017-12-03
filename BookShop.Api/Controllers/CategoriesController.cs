namespace BookShop.Api.Controllers
{
    using BookShop.Api.Infrastructure.Exstensions;
    using BookShop.Api.Infrastructure.Filters;
    using BookShop.Api.Models;
    using BookShop.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoryService category;

        public CategoriesController(ICategoryService category)
        {
            this.category = category;
        }

        [HttpGet(WebConstants.ById)]
        public IActionResult Get(int id)
            => this.OkOrNotFound(this.category.ById(id));

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this.category.All());
        }

        [HttpPut(WebConstants.ById)]
        [ValidateModelState]
        public IActionResult Put(int id, [FromBody]CategoryRequestModel model)
        {
            if (!this.category.Exists(id))
            {
                return this.BadRequest("Category does not exists!");
            }
            else if (this.category.Duplicate(model.Name))
            {
                return this.BadRequest("Duplicate category!");
            }

            bool successfullyUpdated = this.category.Update(
                id,
                model.Name);

            if (!successfullyUpdated)
            {
                return this.BadRequest("Unsuccessful update!");
            }

            return this.Ok();
        }


        [HttpPost]
        [ValidateModelState]
        public IActionResult Post([FromBody]CategoryRequestModel model)
        {
            if (this.category.Duplicate(model.Name))
            {
                return this.BadRequest("Duplicate category!");
            }

            var id = this.category.Create(model.Name);

            return this.Ok(id);
        }

        [HttpDelete(WebConstants.ById)]
        public IActionResult Delete(int id)
        {
            if (!this.category.Exists(id))
            {
                return this.BadRequest("Book does not exists!");
            }

            bool successfullyDeleted = this.category.Delete(id);

            if (!successfullyDeleted)
            {
                return this.BadRequest("Delete failed!");
            }

            return this.Ok(id);
        }
    }
}