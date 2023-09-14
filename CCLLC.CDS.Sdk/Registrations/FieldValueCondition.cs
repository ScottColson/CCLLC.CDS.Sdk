namespace CCLLC.CDS.Sdk.Registrations
{
    public enum ImageType { Target = 1, PreImage = 2, CoalescedImage = 3 }

    public class TargetValueCondition<TParent> : FieldValueCondition<TParent>, IExecutionFilterValueCondition<TParent> where TParent : IExecutionFilter
    {
        public TargetValueCondition(IExecutionFilter parentFilter, string field) 
            : base(parentFilter, ImageType.Target, field)
        {
        }
    }

    public class PreImageValueCondition<TParent> : FieldValueCondition<TParent>, IExecutionFilterValueCondition<TParent> where TParent : IExecutionFilter
    {
        public PreImageValueCondition(IExecutionFilter parentFilter, string field) 
            : base(parentFilter, ImageType.PreImage, field)
        {
        }
    }

    public class CoalescedValueCondition<TParent> : FieldValueCondition<TParent>, IExecutionFilterValueCondition<TParent> where TParent : IExecutionFilter
    {
        public CoalescedValueCondition(IExecutionFilter parentFilter, string field) 
            : base(parentFilter, ImageType.CoalescedImage, field)
        {
        }
    }

    public abstract class FieldValueCondition<TParent> : ExecutionFilterCondition, IExecutionFilterValueCondition<TParent> where TParent : IExecutionFilter
    {
        public ImageType ImageType { get; }
        private IExecutionFilter parent;
        private FieldValueTest valueTest;
        private string fieldName;

        protected FieldValueCondition(IExecutionFilter parentFilter, ImageType imageType, string field)
        {
            parent = parentFilter;
            fieldName = field;
            ImageType = imageType;
        }
        
        public TParent IsNull()
        {
            valueTest = new FieldValueNullTest(false);
            return (TParent)parent;
        }

        public TParent IsNotNull()
        {
            valueTest = new FieldValueNullTest(true);
            return (TParent)parent;
        }

        public TParent IsEqualTo<T>(params T[] values)
        {
            valueTest = new FieldValueEqualityTest<T>(values, false);
            return (TParent)parent;
        }

        public TParent IsNotEqualTo<T>(params T[] values)
        {
            valueTest = new FieldValueEqualityTest<T>(values, true);
            return (TParent)parent;
        }

        public TParent IsGreaterThan<T>(T value)
        {
            valueTest = new FieldValueGreaterThanTest<T>(value, false);
            return (TParent)parent;
        }

        public TParent IsGreaterThanOrEqualTo<T>(T value)
        {
            valueTest = new FieldValueGreaterThanTest<T>(value, true);
            return (TParent)parent;
        }

        public TParent IsLessThan<T>(T value)
        {
            valueTest = new FieldValueLessThanTest<T>(value, false);
            return (TParent)parent;
        }

        public TParent IsLessThanOrEqualTo<T>(T value)
        {
            valueTest = new FieldValueLessThanTest<T>(value, true);
            return (TParent)parent;
        }

        public TParent IsLike(params string[] values)
        {
            valueTest = new FieldValueLikeTest(values);
            return (TParent)parent;
        }

        public override bool TestCondition(ICDSPluginExecutionContext executionContext)
        {
            var image =
                (ImageType == ImageType.CoalescedImage) ? executionContext.PreMergedTarget
                : (ImageType == ImageType.PreImage) ? executionContext.PreImage
                : executionContext.TargetEntity;

            return valueTest.Test(image, fieldName);
        }
    }
}
