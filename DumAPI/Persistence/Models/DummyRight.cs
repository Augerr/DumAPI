namespace DumAPI.Persistence.Models;

public partial class DummyRight
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<DummyUserRight> DummyUserRights { get; set; } = new List<DummyUserRight>();
}
