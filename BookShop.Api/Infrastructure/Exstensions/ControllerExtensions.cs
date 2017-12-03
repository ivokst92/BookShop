namespace BookShop.Api.Infrastructure.Exstensions
{
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static IActionResult OkOrNotFound(this Controller controller, object model)
        {
            if (model == null)
            {
                return controller.NotFound();
            }

            return controller.Ok(model);
        }
    }
}
