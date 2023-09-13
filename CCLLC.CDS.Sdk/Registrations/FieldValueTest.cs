namespace CCLLC.CDS.Sdk.Registrations
{
    using Microsoft.Xrm.Sdk;

    public abstract class FieldValueTest
    {
        public abstract bool Test(Entity image, string fieldName);
    }

    public class FieldValueNullTest : FieldValueTest
    {
        private bool invertResult = false;

        public FieldValueNullTest(bool invert) : base()
        {
            invertResult = invert;
        }

        public override bool Test(Entity image, string fieldName)
        {
            if (fieldName is null
                || image is null
                || image.Contains(fieldName) != true)
            {
                return false;
            }

            if (image.Contains(fieldName) != true)
            {
                return false ^ invertResult;
            }

            object imageValue = image[fieldName];
            bool isNull = imageValue is null;

            return isNull ^ invertResult;
        }
    }

    public class FieldValueEqualityTest<T> : FieldValueTest
    {
        private T[] testValues = null;
        private bool invertResult = false;

        public FieldValueEqualityTest(T[] values, bool invert) : base()
        {
            testValues = values;
            invertResult = invert;
        }

        public override bool Test(Entity image, string fieldName)
        {
            if (fieldName is null
                || image is null
                || image.Contains(fieldName) != true
                || testValues is null
                || testValues.Length == 0)
            {
                return false;
            }

            T imageValue = image.GetAttributeValue<T>(fieldName);

            foreach (T testValue in testValues)
            {
                if (typeof(T) == typeof(string)
                    && string.Compare(imageValue.ToString(), testValue.ToString(), true) == 0)
                {
                    return true ^ invertResult;
                }
                else if (imageValue.Equals(testValue))
                {
                    return true ^ invertResult;
                }
            }

            return false ^ invertResult;
        }
    }

    public class FieldValueGreaterThanTest<T> : FieldValueTest
    {
        T testValue;
        bool testEquality = false;

        public FieldValueGreaterThanTest(T value, bool testEquality) : base()
        {
            testValue = value;
            this.testEquality = testEquality;
        }

        public override bool Test(Entity image, string fieldName)
        {
            if (image.Contains(fieldName) != true)
            {
                return false;
            }

            T imageValue = image.GetAttributeValue<T>(fieldName);

            if (typeof(T) == typeof(string))
            {
                var position = string.Compare((string)(object)testValue, (string)(object)imageValue, true);

                if (position > 0) return true;
                if (testEquality && position == 0) return true;
            }

            if (typeof(T) == typeof(decimal) 
                || typeof(T) == typeof(int))
            {
                if ((decimal)(object)imageValue > (decimal)(object)testValue) return true;
                if (testEquality && (decimal)(object)imageValue == (decimal)(object)testValue) return true;
            }

            return false;

        }
    }

    public class FieldValueLessThanTest<T> : FieldValueTest
    {
        T testValue;
        bool testEquality = false;

        public FieldValueLessThanTest(T value, bool testEquality) : base()
        {
            testValue = value;
            this.testEquality = testEquality;
        }

        public override bool Test(Entity image, string fieldName)
        {
            T imageValue = image.GetAttributeValue<T>(fieldName);

            if (typeof(T) == typeof(string))
            {
                var position = string.Compare((string)(object)testValue, (string)(object)imageValue, true);

                if (position < 0) return true;
                if (testEquality && position == 0) return true;
            }

            if (typeof(T) == typeof(decimal)
                || typeof(T) == typeof(int))
            {
                if ((decimal)(object)imageValue < (decimal)(object)testValue) return true;
                if (testEquality && (decimal)(object)imageValue == (decimal)(object)testValue) return true;
            }

            return false;
        }
    }

    public class FieldValueLikeTest : FieldValueTest
    {
        private string[] likeValues = null;

        public FieldValueLikeTest(string[] values)
        {
            likeValues = values;
        }

        public override bool Test(Entity image, string fieldName)
        {
            throw new System.NotImplementedException("FieldValueLikeTest.Test is not implemented.");
        }
    }
}
