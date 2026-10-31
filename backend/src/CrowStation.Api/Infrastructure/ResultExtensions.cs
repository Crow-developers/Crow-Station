using CrowStation.Domain.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CrowStation.Api.Infrastructure;

/// <summary>
/// Maps business results to HTTP responses so endpoints stay one line long.
/// </summary>
internal static class ResultExtensions
{
    public static IResult ToHttpResult<TValue>(this Result<TValue> result) =>
        result.IsSuccess ? TypedResults.Ok(result.Value) : result.Error.ToProblem();

    public static IResult ToHttpResult(this Result result) =>
        result.IsSuccess ? TypedResults.NoContent() : result.Error.ToProblem();

    private static ProblemHttpResult ToProblem(this Error error) =>
        TypedResults.Problem(
            title: error.Code,
            detail: error.Message,
            statusCode: error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            });
}
