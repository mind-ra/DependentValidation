using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DependentValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class DependentPropertyValidationAttribute : ValidationAttribute
    {
        public string DependentProperty { get; private set; }
        public DependentPropertyValidationAttribute(string dependentProperty)
        {
            DependentProperty = dependentProperty;
        }

        protected abstract override ValidationResult IsValid(object value, ValidationContext context);

        public abstract override string FormatErrorMessage(string name);

        public virtual string DefaultErrorMessage
        {
            get { return "{0} is invalid due to {1}."; }
        }

        protected bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
    }
}
