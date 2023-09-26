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

            object imageValue = image[fieldName];

            foreach (T testValue in testValues)
            {
                bool testResult = false;

                if (typeof(T) == typeof(string))
                {
                    string stringValue = (string)imageValue;
                    testResult = string.Compare(imageValue.ToString(), testValue.ToString(), true) == 0;                
                }
                else if (imageValue.GetType() == typeof(OptionSetValue))
                {
                    int? optionValue = ((OptionSetValue)imageValue)?.Value;

                    if (typeof(T).IsEnum == true || typeof(T) == typeof(int))
                    {
                        testResult = optionValue == (int)(object)testValue;
                    }
                }
                else if (imageValue.GetType() == typeof(Money))
                {
                    decimal? decimalValue = ((Money)imageValue)?.Value;

                    if (typeof(T) == typeof(decimal))
                    {
                        testResult = decimalValue == (decimal)(object)testValue;
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        testResult = decimalValue == (int)(object)(testValue);
                    }

                }
                else if (imageValue.Equals(testValue))
                {
                    testResult = true;
                }

                if (testResult == true)
                {
                    return testResult ^ invertResult;
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
            if (fieldName is null
                || image is null
                || image.Contains(fieldName) != true
                || testValue == null)
            {
                return false;
            }

            object imageValue = image[fieldName];

            if (imageValue == null)
            {
                return false;
            }

            else if (typeof(T) == typeof(string))
            {
                var position = string.Compare((string)(object)testValue, (string)(object)imageValue, true);

                if (position > 0) return true;
                if (testEquality && position == 0) return true;
            }

            else if (imageValue.GetType() == typeof(Money))
            {
                decimal? decimalValue = ((Money)imageValue)?.Value;

                if (typeof(T) == typeof(decimal))
                {
                    if (decimalValue > (decimal)(object)testValue) return true;
                    if (testEquality && decimalValue == (decimal)(object)testValue) return true;
                }

                if (typeof(T) == typeof(int))
                {
                    if (decimalValue > (int)(object)testValue) return true;
                    if (testEquality && decimalValue == (int)(object)testValue) return true;
                }

                if (typeof(T) == typeof(double))
                {
                    if ((double)decimalValue > (double)(object)testValue) return true;
                    if (testEquality && (double)decimalValue == (double)(object)testValue) return true;
                }
            }

            else if (imageValue.GetType() == typeof(int))
            {
                if ((int)imageValue > (int)(object)testValue) return true;
                if (testEquality && (int)imageValue == (int)(object)testValue) return true;
            }

            else if (typeof(T) == typeof(double))
            {
                if ((double)imageValue > (double)(object)testValue) return true;
                if (testEquality && (double)imageValue == (double)(object)testValue) return true;
            }

            else if (typeof(T) == typeof(decimal))
            {
                if ((decimal)imageValue > (decimal)(object)testValue) return true;
                if (testEquality && (decimal)imageValue == (decimal)(object)testValue) return true;
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
            if (fieldName is null
                || image is null
                || image.Contains(fieldName) != true
                || testValue == null)
            {
                return false;
            }

            object imageValue = image[fieldName];

            if (imageValue == null)
            {
                return false;
            }

            else if (typeof(T) == typeof(string))
            {
                var position = string.Compare((string)(object)testValue, (string)(object)imageValue, true);

                if (position < 0) return true;
                if (testEquality && position == 0) return true;
            }

            else if (imageValue.GetType() == typeof(Money))
            {
                decimal? decimalValue = ((Money)imageValue)?.Value;

                if (typeof(T) == typeof(decimal))
                {
                    if (decimalValue < (decimal)(object)testValue) return true;
                    if (testEquality && decimalValue == (decimal)(object)testValue) return true;
                }

                if (typeof(T) == typeof(int))
                {
                    if (decimalValue < (int)(object)testValue) return true;
                    if (testEquality && decimalValue == (int)(object)testValue) return true;
                }

                if (typeof(T) == typeof(double))
                {
                    if ((double)decimalValue < (double)(object)testValue) return true;
                    if (testEquality && (double)decimalValue == (double)(object)testValue) return true;
                }
            }

            else if (imageValue.GetType() == typeof(int))
            {
                if ((int)imageValue < (int)(object)testValue) return true;
                if (testEquality && (int)imageValue == (int)(object)testValue) return true;
            }

            else if (typeof(T) == typeof(double))
            {
                if ((double)imageValue < (double)(object)testValue) return true;
                if (testEquality && (double)imageValue == (double)(object)testValue) return true;
            }

            else if (typeof(T) == typeof(decimal))
            {
                if ((decimal)imageValue < (decimal)(object)testValue) return true;
                if (testEquality && (decimal)imageValue == (decimal)(object)testValue) return true;
            }


            return false;

        }
       
    }

    public class FieldValueLikeTest : FieldValueTest
    {
        private string[] testValues = null;

        public FieldValueLikeTest(string[] values)
        {
            testValues = values;
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

            string imageValue = image.GetAttributeValue<string>(fieldName);

            if (string.IsNullOrEmpty(imageValue))
            {
                return false;
            }

            foreach (string testValue in testValues)
            {
                bool allPartsMatch = true;
                string[] parts = testValue.Split('*');
                foreach(var part in parts)
                {
                    if(false == imageValue.Contains(part))
                    {
                        allPartsMatch = false;
                        break;
                    }
                }

                if (allPartsMatch)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
