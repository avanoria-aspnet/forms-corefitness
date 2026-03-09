using Application.Common.Results;
using Application.CustomerService.ContactRequests.Inputs;
using Domain.Abstractions.Repositories.CustomerService;
using Domain.Aggregates.CustomerService;
using System.Diagnostics;

namespace Application.CustomerService.ContactRequests;

public sealed class ContactRequestService(IContactRequestRepository repo) : IContactRequestService
{
    public async Task<Result> CreateContactRequestAsync(ContactRequestInput input, CancellationToken ct = default)
    {   
        try
        {
            if (input is null)
                return Result.Error("input model must be provided");

            var model = ContactRequest.Create(
                input.FirstName,
                input.LastName,
                input.Email,
                input.PhoneNumber,
                input.Message
            );

            await repo.AddAsync(model, ct);
            return Result.Ok();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);

            return Result.Error();
        }
    }

    public async Task<Result> DeleteContactRequestAsync(string id, CancellationToken ct = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                return Result.Error("Id must be provided");

            var model = await repo.GetByIdAsync(id, ct);
            if (model is null)
                return Result.NotFound($"Contact Request with id '{id}' was not found.");

            var deleted = await repo.DeleteAsync(model, ct);
            return deleted ? Result.Ok() : Result.Error();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);

            return Result.Error();
        }
    }

    public async Task<Result<ContactRequest?>> GetContactRequestAsync(string id, CancellationToken ct = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                return Result<ContactRequest?>.Error("Id must be provided");

            var model = await repo.GetByIdAsync(id, ct);
            return model is null
                ? Result<ContactRequest?>.NotFound($"Contact Request with id '{id}' was not found.")
                : Result<ContactRequest?>.Ok(model);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);

            return Result<ContactRequest?>.Error();
        }
    }

    public async Task<Result<IReadOnlyList<ContactRequest>>> GetContactRequestsAsync(string id, CancellationToken ct = default)
    {
        try
        {
            var models = await repo.GetAllAsync(ct);
            return Result<IReadOnlyList<ContactRequest>>.Ok(models);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);

            return Result<IReadOnlyList<ContactRequest>>.Error();
        }
    }

    public async Task<Result> MarkAsReadAsync(string id, CancellationToken ct = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                return Result.Error("Id must be provided");

            var model = await repo.GetByIdAsync(id, ct);
            if (model is null)
                return Result.NotFound($"Contact Request with id '{id}' was not found.");

            model.MarkAsRead();

            var updated = await repo.UpdateAsync(model, ct);
            return updated ? Result.Ok() : Result.Error();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);

            return Result.Error();
        }
    }

    public async Task<Result> MarkAsUnreadAsync(string id, CancellationToken ct = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                return Result.Error("Id must be provided");

            var model = await repo.GetByIdAsync(id, ct);
            if (model is null)
                return Result.NotFound($"Contact Request with id '{id}' was not found.");

            model.MarkAsUnread();

            var updated = await repo.UpdateAsync(model, ct);
            return updated ? Result.Ok() : Result.Error();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);

            return Result.Error();
        }
    }
}
