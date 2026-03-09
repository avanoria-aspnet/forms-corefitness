using Domain.Abstractions.Repositories.CustomerService;
using Domain.Aggregates.CustomerService;
using Infrastructure.Persistence.EfCore.Contexts;
using Infrastructure.Persistence.EfCore.Entities;

namespace Infrastructure.Persistence.EfCore.Repositories.CustomerService;

public sealed class ContactRequestRepository(DataContext context) : RepositoryBase<ContactRequest, string, ContactRequestEntity, DataContext>(context), IContactRequestRepository
{
    protected override void ApplyUpdates(ContactRequest model, ContactRequestEntity entity)
    {
        entity.MarkedAsRead = model.MarkedAsRead;
    }

    protected override string GetId(ContactRequest model)
    {
        return model.Id;
    }

    protected override ContactRequestEntity ToEntity(ContactRequest model)
    {
        var entity = new ContactRequestEntity()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Message = model.Message,
            CreatedAt = model.CreatedAt,
            MarkedAsRead = model.MarkedAsRead
        };

        return entity;
    }

    protected override ContactRequest ToModel(ContactRequestEntity entity)
    {
        var model = ContactRequest.Rehydrate
            (
             entity.Id,
             entity.FirstName,
             entity.LastName,
             entity.Email,
             entity.PhoneNumber,
             entity.Message,
             entity.CreatedAt,
             entity.MarkedAsRead
            );

        return model;
    }

}
