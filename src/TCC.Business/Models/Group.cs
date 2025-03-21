﻿namespace TCC.Business.Models
{
    public class Group : Entity
    {
        public string Description { get; set; }
        public bool Active { get; set; }

        //EF Relation
        public IEnumerable<UserGroup> UserGroups { get; set; }
    }
}
