# DependentValidation

DependentValidation is a .netstandard2.0 library that extends the System.ComponentModel.DataAnnotations with conditional attributes based on another property of the validated object.

The new attributes are the following:

- RequiredIf
- RequiredIfEmpty
- RequiredIfFalse
- RequiredIfNot
- RequiredIfNotEmpty
- RequiredIfNotRegExMatch
- RequiredIfRegExMatch
- RequiredIfTrue
- RegularExpressionIf
- Is
- EqualTo
- GreaterThan
- GreaterThanOrEqualTo
- LessThan
- LessThanOrEqualTo
- NotEqualTo

--- 

### Installation

DependentValidation is on [NuGet](https://www.nuget.org/packages/DependentValidation/)

``` powershell
Install-Package Bogus
```
Minimum Requirements: **.NET Standard 2.0**.

--- 

Inside this project there is a small js file, [dependentvalidation.unobstrusive.js](https://github.com/mind-ra/DependentValidation/blob/master/dependentvalidation.unobstrusive.js) that extend the [jQuery Validation Plugin](https://jqueryvalidation.org/) and the [jQuery Unobtrusive Validation](https://github.com/aspnet/jquery-validation-unobtrusive) for unobstrusive client validation.