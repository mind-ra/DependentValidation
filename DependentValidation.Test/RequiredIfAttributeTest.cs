using NUnit.Framework;

namespace DependentValidation.Test
{
    public class RequiredIfAttributeTest
    {
        private class Model : ModelBase<RequiredIfAttribute>
        {
            public string Value1 { get; set; }

            [RequiredIf("Value1", "hello")]
            public string Value2 { get; set; }
        }

        [Test]
        public void IsValidTest()
        {
            var model = new Model() { Value1 = "hello", Value2 = "asd" };
            Assert.IsTrue(model.IsValid("Value2"));
        }

        [Test]
        public void IsNotValidTest()
        {
            var model = new Model() { Value1 = "hello", Value2 = "" };
            Assert.IsFalse(model.IsValid("Value2"));
        }

        [Test]
        public void IsNotValidWithValue2NullTest()
        {
            var model = new Model() { Value1 = "hello", Value2 = null };
            Assert.IsFalse(model.IsValid("Value2"));
        }

        [Test]
        public void IsNotRequiredTest()
        {
            var model = new Model() { Value1 = "goodbye" };
            Assert.IsTrue(model.IsValid("Value2"));
        }

        [Test]
        public void IsNotRequiredWithValue1NullTest()
        {
            var model = new Model() { Value1 = null };
            Assert.IsTrue(model.IsValid("Value2"));
        }
    }
}