using TCC.Business.Models;

namespace TCC.Application.Views
{
    public class InviteView : IdView
    {
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
        public InviteStatus Status { get; set; }
    }
}
