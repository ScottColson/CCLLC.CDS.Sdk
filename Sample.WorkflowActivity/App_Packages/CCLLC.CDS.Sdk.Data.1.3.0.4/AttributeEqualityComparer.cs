using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    public class AttributeEqualityComparer : IAttributeEqualityComparer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public new bool Equals(object x, object y)
        {
            if ((x is null || (x.GetType() == typeof(string) && (x as string).Length == 0))
                && (y is null || (y.GetType() == typeof(string) && (y as string).Length == 0)))
                return true;

            if (x is null && y != null
                || x != null && y is null)
                return false;

            if (x.GetType() == y.GetType())
            {
                if (x.GetType() == typeof(OptionSetValue))
                    return ((OptionSetValue)x).Value == ((OptionSetValue)y).Value;

                if (x.GetType() == typeof(BooleanManagedProperty))
                    return ((BooleanManagedProperty)x).Value == ((BooleanManagedProperty)y).Value;

                if (x.GetType() == typeof(EntityReference))
                    return ((EntityReference)x).LogicalName == ((EntityReference)y).LogicalName
                        && ((EntityReference)x).Id == ((EntityReference)y).Id;

                if (x.GetType() == typeof(Money))
                    return (((Money)x).Value == ((Money)y).Value);

                if (x.GetType() == typeof(DateTime) || x.GetType() == typeof(DateTime?))
                    return Math.Abs(((DateTime)x - (DateTime)y).TotalSeconds) < 1;

                return x.Equals(y);
            }

            return false;
        }

        public int GetHashCode(object obj)
        {
            _ = obj ?? throw new ArgumentNullException(nameof(obj));

            return obj.GetHashCode();
        }
    }
}
