﻿namespace TCC.Business.Models
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
                        Guid userId,
                        Guid categoryId,
                        Guid groupId)
        {
            Description = description;
            Value = value;
            BeginDate = beginDate.Date;
            EndDate = endDate.HasValue ? endDate.Value.Date : null;
            Recurrence = recurrence;
            RecurrenceInterval = recurrenceInterval;
            IsRecurring = isRecurring;
            UserId = userId;
            CategoryId = categoryId;
            GroupId = groupId;
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

        public void Update(string description,
                                decimal value,
                                DateTime beginDate,
                                DateTime? endDate,
                                RecurrenceType? recurrence,
                                int recurrenceInterval,
                                bool isRecurring,
                                Guid userId,
                                Guid categoryId)
        {
            Description = description;
            Value = value;
            BeginDate = beginDate.Date;
            EndDate = endDate.HasValue ? endDate.Value.Date : null;
            Recurrence = recurrence;
            RecurrenceInterval = recurrenceInterval;
            IsRecurring = isRecurring;
            UserId = userId;
            CategoryId = categoryId;
        }

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
