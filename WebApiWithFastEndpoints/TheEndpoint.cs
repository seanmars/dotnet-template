using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApiWithFastEndpoints;

public class TheEndpoint : Endpoint<TheRequest,
    Results<Ok<TheResponse>,
        NotFound,
        ProblemDetails>>
{
    public override void Configure()
    {
        Post("/api/user/create");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<TheResponse>, NotFound, ProblemDetails>> ExecuteAsync(
        TheRequest req, CancellationToken ct)
    {
        await Task.CompletedTask; //simulate async work

        if (req.Id == 0) //condition for a not found response
        {
            return TypedResults.NotFound();
        }

        if (req.Id == 1) //condition for a problem details response
        {
            AddError(r => r.Id, "value has to be greater than 1");
            return new FastEndpoints.ProblemDetails(ValidationFailures);
        }

        // 200 ok response with a DTO
        return TypedResults.Ok(new TheResponse
        {
            RequestedId = req.Id,
            FullName = $"{req.FirstName} {req.LastName}",
            IsOver18 = req.Age >= 18
        });
    }
}