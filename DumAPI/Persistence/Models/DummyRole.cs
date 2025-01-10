namespace DumAPI.Persistence.Models;

public partial class DummyRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<DummyUser> DummyUsers { get; set; } = new List<DummyUser>();
}
