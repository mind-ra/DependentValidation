using System.ComponentModel.DataAnnotations;

namespace DependentValidation.Test
{
    abstract class ModelBase<T> where T : DependentPropertyValidationAttribute
    {
        public T GetAttribute(string property)
        {
            return (T)this.GetType().GetProperty(property).GetCustomAttributes(typeof(T), false)[0];
        }

        public bool IsValid(string property)
        {
            var attribute = this.GetAttribute(property);
            var propertyValue = this.GetType().GetProperty(property).GetValue(this, null);
            var context = new ValidationContext(this);
            var result = attribute.GetValidationResult(propertyValue, context);
            return result == ValidationResult.Success;
        }
    }
}
