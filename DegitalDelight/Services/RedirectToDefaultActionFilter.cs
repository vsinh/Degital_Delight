using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DegitalDelight.Services
{
    public class RedirectToDefaultActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (actionDescriptor == null)
            {
                return;
            }

            var controllerName = actionDescriptor.ControllerName;
            var actionName = actionDescriptor.ActionName;

            // Check if the action exists
            if (!ActionExists(context, controllerName, actionName))
            {
                // Redirect to a default action or a specific page
                var url = $"/NotFound"; // Change this to your desired default action or page
                context.Result = new RedirectResult(url);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Implementation not needed for this example
        }

        private bool ActionExists(ActionExecutingContext context, string controllerName, string actionName)
        {
            var actionSelector = context.HttpContext.RequestServices.GetService(typeof(IActionDescriptorCollectionProvider)) as IActionDescriptorCollectionProvider;
            var actionDescriptor = actionSelector?.ActionDescriptors.Items.FirstOrDefault(a =>
                a is ControllerActionDescriptor cad &&
                cad.ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase) &&
                cad.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase));

            return actionDescriptor != null;
        }
    }
}
