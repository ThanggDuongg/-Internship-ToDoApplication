using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Web.Http.Controllers;
using TodoApplication.Models;

namespace TodoApplication.Filters
{
    public class ValidationFilter : Attribute   //IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(new Response<Exception>()
                {
                    Success = false,
                    Message = "Something wrong!",
                    Errors = context.ModelState.Values.SelectMany(v => v.Errors),
                });
            }
        }
    }
}
