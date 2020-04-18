
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DependentValidation
{
    public class RequiredIfRegExMatchAttribute : RequiredIfAttribute
    {
        public RequiredIfRegExMatchAttribute(string dependentProperty, string pattern) : base(dependentProperty, Operator.RegExMatch, pattern) { }
    }
}
