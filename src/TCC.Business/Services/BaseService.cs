using FluentValidation;
using FluentValidation.Results;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Business.Notifications;

namespace TCC.Business.Services
{
    public abstract class BaseService(INotifier notifier)
    {
        private readonly INotifier _notifier = notifier;

        protected void Notify(ValidationResult validationResult) => validationResult.Errors.ForEach(err => Notify(err.ErrorMessage));

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity)
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
