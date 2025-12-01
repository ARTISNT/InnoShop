namespace ProductsManagement.Application.Abstractions;

public interface IUserContext
{
    Guid GetUserId();
    void EnsureOwnerOrAdmin(Guid ownerId);
}