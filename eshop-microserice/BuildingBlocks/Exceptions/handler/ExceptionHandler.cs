using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class ExceptionHandler
	(ILogger<ExceptionHandler> logger)
	: IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext context,
		System.Exception exception, CancellationToken cancellationToken)
	{
		logger.LogError("Error message: {exceptionMessage}, " +
		"Time of occurrence {time}", exception.Message, DateTimeOffset.UtcNow);

		(string detail, string title, int statusCode) details = exception switch
		{
			InternalSeverException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status500InternalServerError
			),
			BadHttpRequestException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status400BadRequest
			),
			NotFoundException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status404NotFound
			),
			ValidationException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status400BadRequest
			),
			_ =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status500InternalServerError
			)
		};

		var problemDetails = new ProblemDetails
		{
			Detail = details.detail,
			Status = details.statusCode,
			Title = details.title,
			Instance = context.Request.Path
		};
		problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

		if (exception is ValidationException validationException)
		{
			problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
		}

		await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
		return true;	
	}
}
