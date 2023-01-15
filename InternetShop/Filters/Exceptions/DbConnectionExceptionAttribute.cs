using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InternetShop.Presentation.Filters.Exceptions 
{
    public class DbConnectionExceptionAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is Neo4j.Driver.ServiceUnavailableException)
            {
                context.Result = new JsonResult("Map database is off. Please report this issue to the company.");
            }
            if(context.Exception is CouchDB.Driver.Exceptions.CouchException)
            {
                context.Result = new JsonResult("Invoice database is off. Cannot proceede with the order. Please report this issue to the company.");
            }
        }
    }
}
