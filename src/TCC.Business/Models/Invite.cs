namespace TCC.Business.Models
{
    public class Invite : Entity
    {
        public Invite() { }
        public Invite(Guid userId, Guid groupId)
        {
            UserId = userId;
            GroupId = groupId;
        }
        public Guid UserId { get; private set; }
        public Guid GroupId { get; private set; }
        public bool IsAccepted => Status == InviteStatus.Accepted;
        public bool IsRejected => Status == InviteStatus.Rejected;
        public bool IsPending => Status == InviteStatus.Pending;
        public InviteStatus Status { get; private set; } = InviteStatus.Pending;

        public void Accept()
        {
            Status = InviteStatus.Accepted;
        }

        public void Reject()
        {
            Status = InviteStatus.Rejected;
        }

        /// EF Relations

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }

    public enum InviteStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }
}
