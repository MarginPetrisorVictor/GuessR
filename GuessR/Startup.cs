using Microsoft.AspNetCore.Builder;

namespace GuessR
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            /*
             * app.Use(async (context, next) =>
             {
                 context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                 context.Response.Headers["Pragma"] = "no-cache";
                 context.Response.Headers["Expires"] = "0";

                 await next();
             });
            */
        }

    }
}
