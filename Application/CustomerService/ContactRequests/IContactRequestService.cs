using Application.Common.Results;
using Application.CustomerService.ContactRequests.Inputs;
using Domain.Aggregates.CustomerService;

namespace Application.CustomerService.ContactRequests;

public interface IContactRequestService
{
    Task<Result> CreateContactRequestAsync(ContactRequestInput input, CancellationToken ct = default);
    Task<Result> MarkAsReadAsync(string id, CancellationToken ct = default);
    Task<Result> MarkAsUnreadAsync(string id, CancellationToken ct = default);
    Task<Result> DeleteContactRequestAsync(string id, CancellationToken ct = default);

    Task<Result<ContactRequest?>> GetContactRequestAsync(string id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<ContactRequest>>> GetContactRequestsAsync(string id, CancellationToken ct = default);
}
