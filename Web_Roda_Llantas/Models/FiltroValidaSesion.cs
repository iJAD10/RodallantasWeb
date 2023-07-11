using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_Roda_Llantas.Models
{
    public class FiltroValidaSesion : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("Token") == null)
            {
                context.Cancel = true;
                context.HttpContext.Response.Redirect("./Index");
            }

            base.OnResultExecuting(context);
        }
    }
}
