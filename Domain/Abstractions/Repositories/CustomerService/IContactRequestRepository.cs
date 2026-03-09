using Domain.Aggregates.CustomerService;

namespace Domain.Abstractions.Repositories.CustomerService;

public interface IContactRequestRepository : IRepositoryBase<ContactRequest, string>
{

}
