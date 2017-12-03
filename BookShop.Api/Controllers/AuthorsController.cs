namespace BookShop.Api.Controllers
{
    using BookShop.Api.Infrastructure.Exstensions;
    using BookShop.Api.Infrastructure.Filters;
    using BookShop.Api.Models.Authors;
    using BookShop.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorsController : BaseController
    {
        private readonly IAuthorService authors;
        public AuthorsController(IAuthorService authors)
        {
            this.authors = authors;
        }
        
        [HttpGet(WebConstants.ById)]
        public IActionResult Get(int id)
        => this.OkOrNotFound(this.authors.Details(id));

        [HttpGet(WebConstants.ById+"/books")]
        public IActionResult GetBooks(int id)
        => Ok(this.authors.Books(id));

        [HttpPost]
        [ValidateModelState]
        public IActionResult Post([FromBody]AuthorRequestModel model)
        => Ok(this.authors.Create(
                model.FirstName,
                model.LastName));
    }
}