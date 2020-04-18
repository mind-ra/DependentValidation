using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DependentValidation
{
    public class RequiredIfAttribute : DependentPropertyValidationAttribute, IClientModelValidator
    {
        public Operator Operator { get; private set; }
        public object ExpectedValue { get; private set; }
        protected OperatorMetadata Metadata { get; private set; }

        public RequiredIfAttribute(string dependentProperty, Operator @operator, object expectedValue)
            : base(dependentProperty)
        {
            Operator = @operator;
            ExpectedValue = expectedValue;
            Metadata = OperatorMetadata.Get(Operator);
        }

        public RequiredIfAttribute(string dependentProperty, object expectedValue)
            : this(dependentProperty, Operator.EqualTo, expectedValue) { }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object dependentValue = context.GetDependentValue(DependentProperty);
            if (Metadata.IsValid(ExpectedValue, dependentValue))
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

            return string.Format(ErrorMessageString, name, DependentProperty, ExpectedValue);
        }

        public override string DefaultErrorMessage
        {
            get { return "{0} is required due to {1} being " + Metadata.ErrorMessage + " {2}"; }
        }

        public virtual void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-requiredif", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredif-dependentproperty", DependentProperty.ToString());
            MergeAttribute(context.Attributes, "data-val-requiredif-expectedvalue", ExpectedValue.ToString());
            MergeAttribute(context.Attributes, "data-val-requiredif-operator", Operator.ToString());
        }
    }
}
