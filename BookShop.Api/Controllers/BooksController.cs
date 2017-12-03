namespace BookShop.Api.Controllers
{
    using BookShop.Api.Infrastructure.Exstensions;
    using BookShop.Api.Infrastructure.Filters;
    using BookShop.Api.Models.Books;
    using BookShop.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
    {
        private readonly IBookService books;
        private readonly IAuthorService authors;
        public BooksController(IBookService books, IAuthorService authors)
        {
            this.books = books;
            this.authors = authors;
        }

        [HttpGet(WebConstants.ById)]
        public IActionResult Get(int id)
       => this.OkOrNotFound(this.books.Details(id));

        [HttpGet]
        public IActionResult Get([FromQuery]string search)
        {
            return this.Ok(this.books.All(search));
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Post([FromBody]CreateBookRequestModel model)
        {
            if (!this.authors.Exists(model.AuthorId))
            {
                return BadRequest("Autor does not exist!");
            }

            var id = this.books.Create(model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.Categories);

            return this.Ok(id);
        }

        [HttpPut(WebConstants.ById)]
        [ValidateModelState]
        public IActionResult Put(int id, [FromBody]BookRequestModel model)
        {
            if (!this.books.Exists(id))
            {
                return this.BadRequest("Book does not exists!");
            }

            bool successfullyUpdated = this.books.Update(
                id,
                model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId);

            if (!successfullyUpdated)
            {
                return this.BadRequest("Unsuccessful update!");
            }

            return this.Ok();
        }

        [HttpDelete(WebConstants.ById)]
        public IActionResult Delete(int id)
        {
            if (!this.books.Exists(id))
            {
                return this.BadRequest("Book does not exists!");
            }

            bool successfullyDeleted = this.books.Delete(id);

            if (!successfullyDeleted)
            {
                return this.BadRequest("Delete failed!");
            }

            return this.Ok(id);
        }
    }
}