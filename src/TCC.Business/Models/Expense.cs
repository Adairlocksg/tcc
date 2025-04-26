namespace TCC.Business.Models
{
    public class Expense : Entity
    {
        public Expense() { }
        public Expense(string description,
                        decimal value,
                        DateTime beginDate,
                        DateTime? endDate,
                        RecurrenceType? recurrence,
                        int recurrenceInterval,
                        bool isRecurring,
                        User user,
                        Category category,
                        Group group)
        {
            Description = description;
            Value = value;
            BeginDate = beginDate;
            EndDate = endDate;
            Recurrence = recurrence;
            RecurrenceInterval = recurrenceInterval;
            IsRecurring = isRecurring;
            UserId = group.Id;
            CategoryId = category.Id;
            GroupId = group.Id;
            User = user;
            Category = category;
            Group = group;
        }

        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public DateTime BeginDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsRecurring { get; private set; } = false;
        public RecurrenceType? Recurrence { get; private set; }
        public int RecurrenceInterval { get; private set; }
        public bool Active { get; private set; } = true;
        public Guid UserId { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid GroupId { get; private set; }

        //EF Relation

        public virtual User User { get; private set; }
        public virtual Category Category { get; private set; }
        public virtual Group Group { get; private set; }
    }

    public enum RecurrenceType
    {
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Custom
    }
}
