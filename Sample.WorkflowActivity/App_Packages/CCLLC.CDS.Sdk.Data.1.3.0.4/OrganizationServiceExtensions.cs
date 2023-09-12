using System;
using System.Linq.Expressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    public static partial class Extensions
    {
        /// <summary>
        /// Execute a Fluent Query.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="organizationService"></param>
        /// <returns></returns>
        public static IExecutableFluentQuery<TEntity> Query<TEntity>(this IOrganizationService organizationService) where TEntity : Entity, new()
        {
            _ = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
           
            var query = new ExecutableFluentQuery<TEntity>(organizationService);
            return query;
        }

        /// <summary>
        /// Retrieve a single early bound record with columns selected through projection.
        /// </summary>        
        public static T GetRecord<T>(this IOrganizationService organizationService, EntityReference recordId, Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            _ = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
            _ = recordId ?? throw new ArgumentNullException(nameof(recordId));
            _ = anonymousTypeInitializer ?? throw new ArgumentNullException(nameof(anonymousTypeInitializer));
            
            var columns = anonymousTypeInitializer.GetAttributeNamesArray<T>();

            return organizationService.GetRecord<T>(recordId, columns);
        }

        /// <summary>
        /// Retrieve a single early bound record with columns identified as string parameters.
        /// </summary> 
        public static T GetRecord<T>(this IOrganizationService organizationService, EntityReference recordId, params string[] columns) where T : Entity
        {
            _ = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
            _ = recordId ?? throw new ArgumentNullException(nameof(recordId));

            return organizationService.GetRecord(recordId, columns).ToEntity<T>();
        }

        /// <summary>
        /// Retrieve an entity record with columns identified as string parameters.
        /// </summary> 
        public static Entity GetRecord(this IOrganizationService organizationService, EntityReference recordId, params string[] columns)
        {
            _ = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
            _ = recordId ?? throw new ArgumentNullException(nameof(recordId));

            var columnSet = (columns.Length == 0) ? new ColumnSet(true) : new ColumnSet(columns);

            var record = organizationService.Retrieve(
                recordId.LogicalName,
                recordId.Id,
                columnSet);

            return record;
        }

    }
}
