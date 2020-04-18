using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DependentValidation
{
    public class OperatorMetadata
    {
        public string ErrorMessage { get; set; }
        public Func<object, object, bool> IsValid { get; set; }

        static OperatorMetadata()
        {
            CreateOperatorMetadata();
        }

        private static Dictionary<Operator, OperatorMetadata> _operatorMetadata;

        public static OperatorMetadata Get(Operator @operator)
        {
            return _operatorMetadata[@operator];
        }

        private static void CreateOperatorMetadata()
        {
            _operatorMetadata = new Dictionary<Operator, OperatorMetadata>()
            {
                {
                    Operator.EqualTo, new OperatorMetadata()
                    {
                        ErrorMessage = "equal to",
                        IsValid = (expectedValue, dependentValue) => {
                            if (expectedValue == null && dependentValue == null)
                                return true;
                            else if (expectedValue == null && dependentValue != null)
                                return false;

                            return expectedValue.Equals(dependentValue);
                        }
                    }
                },
                {
                    Operator.NotEqualTo, new OperatorMetadata()
                    {
                        ErrorMessage = "not equal to",
                        IsValid = (expectedValue, dependentValue) => {
                            if (expectedValue == null && dependentValue != null)
                                return true;
                            else if (expectedValue == null && dependentValue == null)
                                return false;

                            return !expectedValue.Equals(dependentValue);
                        }
                    }
                },
                {
                    Operator.GreaterThan, new OperatorMetadata()
                    {
                        ErrorMessage = "greater than",
                        IsValid = (expectedValue, dependentValue) => {
                            if (expectedValue == null || dependentValue == null)
                                return false;

                            return Comparer<object>.Default.Compare(expectedValue, dependentValue) >= 1;
                        }
                    }
                },
                {
                    Operator.LessThan, new OperatorMetadata()
                    {
                        ErrorMessage = "less than",
                        IsValid = (expectedValue, dependentValue) => {
                            if (expectedValue == null || dependentValue == null)
                                return false;

                            return Comparer<object>.Default.Compare(expectedValue, dependentValue) <= -1;
                        }
                    }
                },
                {
                    Operator.GreaterThanOrEqualTo, new OperatorMetadata()
                    {
                        ErrorMessage = "greater than or equal to",
                        IsValid = (expectedValue, dependentValue) => {
                            if (expectedValue == null && dependentValue == null)
                                return true;

                            if (expectedValue == null || dependentValue == null)
                                return false;

                            return Get(Operator.EqualTo).IsValid(expectedValue, dependentValue) || Comparer<object>.Default.Compare(expectedValue, dependentValue) >= 1;
                        }
                    }
                },
                {
                    Operator.LessThanOrEqualTo, new OperatorMetadata()
                    {
                        ErrorMessage = "less than or equal to",
                        IsValid = (expectedValue, dependentValue) => {
                            if (expectedValue == null && dependentValue == null)
                                return true;

                            if (expectedValue == null || dependentValue == null)
                                return false;

                            return Get(Operator.EqualTo).IsValid(expectedValue, dependentValue) || Comparer<object>.Default.Compare(expectedValue, dependentValue) <= -1;
                        }
                    }
                },
                {
                    Operator.RegExMatch, new OperatorMetadata()
                    {
                        ErrorMessage = "a match to",
                        IsValid = (pattern, dependentValue) => {
                            return Regex.Match(dependentValue.ToString(), (pattern ?? "").ToString()).Success;
                        }
                    }
                },
                {
                    Operator.NotRegExMatch, new OperatorMetadata()
                    {
                        ErrorMessage = "not a match to",
                        IsValid = (pattern, dependentValue) => {
                            return !Regex.Match(dependentValue.ToString(), (pattern ?? "").ToString()).Success;
                        }
                    }
                }
            };
        }
    }
}
