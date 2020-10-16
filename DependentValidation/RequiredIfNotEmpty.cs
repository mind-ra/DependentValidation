
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DependentValidation
{
    public class RequiredIfNotEmptyAttribute : DependentPropertyValidationAttribute, IClientModelValidator
    {
        public RequiredIfNotEmptyAttribute(string dependentProperty)
            : base(dependentProperty) { }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object dependentValue = context.GetDependentValue(DependentProperty);

            if (!string.IsNullOrEmpty((dependentValue ?? string.Empty).ToString().Trim()))
            {
                if (value != null && !string.IsNullOrEmpty(value.ToString().Trim()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(context.DisplayName));
                }
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = DefaultErrorMessage;

            return string.Format(ErrorMessageString, name, DependentProperty);
        }

        public override string DefaultErrorMessage
        {
            get { return "{0} is required due to {1} not being empty."; }
        }

        public virtual void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-requiredif", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredif-dependentproperty", DependentProperty.ToString()); ;
        }
    }
}
