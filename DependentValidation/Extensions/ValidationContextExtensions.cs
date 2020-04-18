using System;
using System.ComponentModel.DataAnnotations;

namespace DependentValidation
{
    public static class ValidationContextExtensions
    {
        public static object GetDependentValue(this ValidationContext context, string dependentProperty)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();
            return type.GetProperty(dependentProperty).GetValue(instance, null);
        }
    }
}
