using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    public static class EntityReferenceExtensions
    {
        /// <summary>
        /// Returns the EntityReference as a typed entity. Fills a gap in the CCLLC.CDS.Sdk.Data package.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityReference"></param>
        /// <returns></returns>
        public static TEntity To<TEntity>(this EntityReference entityReference) where TEntity : Entity, new()
        {
            if (entityReference is null) return null;

            var entity = new TEntity();
            entity.Id = entityReference.Id;
            return entity;
        }

    }
}
