namespace DumAPI.Persistence.Models;

public partial class DummyUserRight
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RightId { get; set; }

    public virtual DummyRight Right { get; set; } = null!;

    public virtual DummyUser User { get; set; } = null!;
}
