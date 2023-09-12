using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    public static class QueryExpressionExtensions
    {        
        /// <summary>
        /// Returns a readable text representation of the query expression for debugging and testing.
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public static string ToReadableText(this QueryExpression qry)
        {
            var level = 1;

            var sb = new StringBuilder();
            sb.AppendLine("Query Expression " + new string('-', 23));
            sb.AppendLine($"Entity = {qry.EntityName}");

            qry.ColumnSet.Parse(sb, 0); 

            sb.AppendLine($"Distinct = {qry.Distinct}");
            sb.AppendLine($"NoLock = {qry.NoLock}");
            sb.AppendLine($"TopCount = {qry.TopCount}");

            qry.Criteria?.Parse(sb, 0);
           
            foreach (var link in qry.LinkEntities)
            {
                link.Parse(sb, level);                
            }

            qry.Orders?.Parse(sb, 0);
            

            sb.AppendLine(new string('-', 40));
            
            return sb.ToString();
        }

        private static void Parse(this ColumnSet columnSet, StringBuilder sb, int parentLevel)
        {
            var spacer = GenerateIndent(parentLevel);
            if (columnSet is null)
            {
                return;
            }

            if (columnSet.AllColumns)
            {
                sb.AppendLine($"{spacer}Columns = All Columns");
            }
            else
            {
                sb.AppendLine($"{spacer}Columns = {string.Join(",", columnSet.Columns.ToArray())}");
            }
        }

        private static void Parse(this FilterExpression filterExpression, StringBuilder sb, int parentLevel)
        {

            var spacer = GenerateIndent(parentLevel);
           
            sb.AppendLine($"{spacer}Filter Operator: {filterExpression.FilterOperator} QuickFind: {filterExpression.IsQuickFindFilter}");

            spacer = GenerateIndent(parentLevel + 1);

            foreach(var c in filterExpression.Conditions)
            {
                sb.AppendLine($"{spacer}Condition: {c.AttributeName} {c.Operator} {string.Join(",", c.Values.ToArray())}");
            }
            
            foreach (var f in filterExpression.Filters)
            {
                f.Parse(sb, parentLevel + 1);                             
            }

            return;
        }

        private static void Parse(this DataCollection<OrderExpression> orders, StringBuilder sb, int parentLevel)
        {
            if(orders.Count <= 0)
            {
                return;
            }

            var spacer = GenerateIndent(parentLevel);
            sb.AppendLine($"{spacer}Order By");

            spacer = GenerateIndent(parentLevel + 1);
            foreach (var o in orders)
            {
                sb.AppendLine($"{spacer}{o.AttributeName}-{o.OrderType}");
            }            
        }

        private static void Parse(this LinkEntity linkEntity, StringBuilder sb, int parentLevel)
        {
            string spacer = GenerateIndent(parentLevel);
            
            sb.AppendLine($"{spacer}LinkedEntity (Join Operator: {linkEntity.JoinOperator.ToString()}) (Alias: {linkEntity.EntityAlias})");

            spacer = GenerateIndent(parentLevel + 1);

            sb.AppendLine($"{spacer}FromEntity = {linkEntity.LinkFromEntityName}");
            sb.AppendLine($"{spacer}FromAttribute = {linkEntity.LinkFromAttributeName}");
            sb.AppendLine($"{spacer}ToEntity = {linkEntity.LinkToEntityName}");
            sb.AppendLine($"{spacer}ToAttribute = {linkEntity.LinkToAttributeName}");

            linkEntity.Columns?.Parse(sb, parentLevel+1);
            linkEntity.LinkCriteria?.Parse(sb, parentLevel+1);
            linkEntity.Orders.Parse(sb, parentLevel+1);
            
            foreach (var child in linkEntity.LinkEntities)
            {
                child.Parse(sb, parentLevel + 1);
            }

            return;
        }
    
        private static string GenerateIndent(int level)
        {
            return new string(' ', 4 * level);
        }
    }
}
