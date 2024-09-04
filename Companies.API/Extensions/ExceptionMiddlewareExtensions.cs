using Domain.Models.Exeptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace Companies.API.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(apperror =>
        {
            apperror.Run(async context =>
            {
                //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if(contextFeature != null)
                {
                    var problemDetailsFactory = app.Services.GetRequiredService<ProblemDetailsFactory>();
                    
                    ProblemDetails problemDetails;
                    int statusCode;

                    switch (contextFeature.Error)
                    {
                        case CompanyNotFoundException companyNotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: companyNotFoundException.Title,
                                    detail: companyNotFoundException.Message);
                            break;
                        default:
                            statusCode = StatusCodes.Status500InternalServerError;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Internal Server Error",
                                    detail: contextFeature.Error.Message);
                             break;
                    }

                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(problemDetails);
                }

            });
        });
    }
}
