namespace DummbAPI.Persistence.Models
{
    public partial class DummyUser
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string? EmployeeId { get; set; }

        public string Email { get; set; } = null!;

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<DummyUserRight> DummyUserRights { get; set; } = new List<DummyUserRight>();

        public virtual DummyRole Role { get; set; } = null!;

        public override string ToString()
        {
            return Username;
        }
    }

}
