using Microsoft.AspNetCore.Components.Web;

namespace TCC.Business.Models
{
    public class UserGroup : Entity
    {
        public UserGroup() { }
        public UserGroup(Guid userId, Guid groupId, bool admin, bool favorite)
        {
            UserId = userId;
            GroupId = groupId;
            Admin = admin;
            Favorite = favorite;
        }
        public bool Admin { get; set; }
        public bool Favorite { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }

        public void FavoriteGroup()
        {
            Favorite = true;
        }

        public void UnfavoriteGroup()
        {
            Favorite = false;
        }

        //EF Relation
        public User User { get; set; }
        public Group Group { get; set; }
    }
}
