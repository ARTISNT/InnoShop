namespace ProductsManagement.Domain.Models;

public class ProductEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Description { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public bool IsDeleted { get; private set; } = false;
    public bool Available { get; private set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public ProductEntity(Guid userId, string name, string description, decimal price)
    {
        SetUserId(userId);
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        Available = true;
        Id = Guid.NewGuid();
    }
    
    private ProductEntity() { }

    public void SetUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty.");

        UserId = userId;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");

        if (name.Length < 3)
            throw new ArgumentException("Name must contain at least 3 characters.");

        Name = name;
    }

    public void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");

        if (description.Length < 10)
            throw new ArgumentException("Description must contain at least 10 characters.");

        Description = description;
    }

    public void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        Price = price;
    }

    public void SetAvailability(bool available)
    {
        Available = available;
    }
}
