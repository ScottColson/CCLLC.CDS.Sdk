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

            object imageValue = image[fieldName];
            bool isNull = imageValue is null;

            return (invertResult == true) ? !isNull : isNull;
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
            bool areEqual = false;

            foreach (T testValue in testValues)
            {
                if (typeof(T) == typeof(string)
                    && string.Compare(imageValue.ToString(), testValue.ToString(), true) == 0)
                {
                    areEqual = true;
                    break;
                }
                else if (imageValue.Equals(testValue))
                {
                    areEqual = true;
                    break;
                }
            }

            return (invertResult == true) ? !areEqual : areEqual;
        }
    }

    public class FieldValueGreaterThanTest<T> : FieldValueTest
    {
        public FieldValueGreaterThanTest(T value, bool testEquality) : base()
        {
            
        }

        public override bool Test(Entity image, string fieldName)
        {
            throw new System.NotImplementedException();
        }
    }

    public class FieldValueLessThanTest<T> : FieldValueTest
    {
        public FieldValueLessThanTest(T value, bool testEquality) : base()
        {

        }

        public override bool Test(Entity image, string fieldName)
        {
            throw new System.NotImplementedException();
        }
    }

    public class FieldValueLikeTest : FieldValueTest
    {
        public FieldValueLikeTest(string[] values)
        {

        }

        public override bool Test(Entity image, string fieldName)
        {
            throw new System.NotImplementedException();
        }
    }
}
