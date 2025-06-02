namespace TCC.Application.Views
{
    public class GroupMemberView : IdView
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Admin { get; set; }
        public bool IsCurrentUser { get; set; }
    }
}
