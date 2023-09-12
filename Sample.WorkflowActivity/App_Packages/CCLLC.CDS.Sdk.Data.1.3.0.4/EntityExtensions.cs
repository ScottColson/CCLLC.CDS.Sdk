using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace CCLLC.CDS.Sdk
{    
    
    /// <summary>
    /// Extensions for the Microsoft.Xrm.Sdk Entity class to provide a set of common functions for manipulating Entity records.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Checks the target for existence of any attribute contained in the provided array of attribute names and returns
        /// true if at least one of the provided attributes exists.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attributeNames"></param>
        /// <returns></returns>
        public static bool ContainsAny(this Entity target, params string[] attributeNames)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));

            foreach (string a in attributeNames)
            {
                if (target.Contains(a))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks an early bound entity for the existence of one or more fields using projection to
        /// define the field list.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="target"></param>
        /// <param name="anonymousTypeInitializer"></param>
        /// <returns></returns>
        public static bool ContainsAny<TEntity>(this TEntity target, Expression<Func<TEntity, object>> anonymousTypeInitializer) where TEntity : Entity
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));
            _ = anonymousTypeInitializer ?? throw new ArgumentNullException(nameof(anonymousTypeInitializer));

            var columns = anonymousTypeInitializer.GetAttributeNamesArray<TEntity>();
            return target.ContainsAny(columns);
        }

        /// <summary>
        /// Checks the target for existence of any attribute not contained in the provided array of attribute names 
        /// and returns true if at least one additional attributes exists.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attributeNames"></param>
        /// <returns></returns>
        public static bool ContainsAnyOtherThan(this Entity target, params string[] attributeNames)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));

            foreach (string k in target.Attributes.Keys)
            {
                if (!attributeNames.Contains(k))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Return an array of fields in the target that are not included in the passed in parameter value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attributeNames"></param>
        /// <returns></returns>
        public static string[] GetFeildsOtherThan(this Entity target, params string[] attributeNames)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));

            var attributes = new List<string>();

            foreach (string k in target.Attributes.Keys)
            {
                if (!attributeNames.Contains(k))
                {
                    attributes.Add(k);
                }
            }

            return attributes.ToArray();
        }

        /// <summary>
        /// Retrieves an attribute from the entity with of the specified type and returns an 
        /// optionally specified default value if the attribute does not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetValue<T>(this Entity target, string key, T defaultValue = default)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));
            _ = key ?? throw new ArgumentNullException(nameof(key));

            if (!target.Contains(key) || target[key] is null)
            {
                return defaultValue;
            }
            
            return target.GetAttributeValue<T>(key);            
        }


        public static T GetAliasedValue<T>(this Entity target, string alias, string fieldName, T defaultValue = default)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));
            _ = alias ?? throw new ArgumentNullException(nameof(alias));
            _ = fieldName ?? throw new ArgumentNullException(nameof(fieldName));

            var key = $"{alias}.{fieldName}";
            return target.GetAliasedValue<T>(key, defaultValue);
            
        }

        public static T GetAliasedValue<T>(this Entity target, string key, T defaultValue = default)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));
            _ = key ?? throw new ArgumentNullException(nameof(key));

            if (!target.Contains(key) || target[key] is null)
            {
                return defaultValue;
            }

            AliasedValue value = target.GetAttributeValue<AliasedValue>(key);
            return (T)value.Value;
        }

        /// <summary>
        /// Returns the aliased fields associated with a joined entity as a early bound entity. When alias is null 
        /// an alias of the return record type is assumed. Will return null if there are no aliased fields for the
        /// return entity type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static T GetAliasedEntity<T>(this Entity target, string alias = null) where T : Entity, new()
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));

            T record = new T();
            var idattribute = record.LogicalName + "id";

            if (string.IsNullOrEmpty(alias))
            {
                alias = record.LogicalName;
            }

            alias += ".";
            foreach (var key in target.Attributes.Keys.Where(k => k.StartsWith(alias)))
            {
                var value = target.GetAttributeValue<AliasedValue>(key).Value;
                var aliasedKey = key.Substring(alias.Length);

                record.Attributes.Add(aliasedKey, value);
   
                if (aliasedKey == idattribute)
                {
                    record.Id = (Guid)value;
                }
            }

            if (record.Attributes.Count == 0)
                return null;

            return record;
        }

        /// <summary>
        /// Merge the entity with another entity to get a more complete list of attributes. If 
        /// the current entity and the source entity both have a value for a given attribute 
        /// then the attribute of the current entity will be preserved.
        /// </summary>
        /// <param name="copyTo"></param>
        /// <param name="copyFrom"></param>
        public static void MergeWith(this Entity copyTo, Entity copyFrom)
        {
            _ = copyTo ?? throw new ArgumentNullException(nameof(copyTo));
            _ = copyFrom ?? throw new ArgumentNullException(nameof(copyFrom));

            copyFrom.Attributes.ToList().ForEach(a =>
            {
                // don't overwrite existing fields in copyTo
                if (!copyTo.Attributes.ContainsKey(a.Key))
                {
                    copyTo.Attributes.Add(a.Key, a.Value);
                }
            });

        }


        /// <summary>
        /// Removes an attribute from the entity attribute collection if that attribute exists.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attributeName"></param>
        public static void RemoveAttribute(this Entity target, string attributeName)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));
            _ = attributeName ?? throw new ArgumentNullException(nameof(attributeName));

            if (target.Contains(attributeName))
            {
                target.Attributes.Remove(attributeName);
            }
        }

        public static void SetState(this IOrganizationService service, Entity target, int state, int status)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));
           
            service.SetState(target, new OptionSetValue(state), new OptionSetValue(status));
        }

        public static void SetState(this IOrganizationService service, Entity target, OptionSetValue state, OptionSetValue status)
        {
            _ = service ?? throw new ArgumentNullException(nameof(service));
            _ = target ?? throw new ArgumentNullException(nameof(target));
            _ = state ?? throw new ArgumentNullException(nameof(state));
            _ = status ?? throw new ArgumentNullException(nameof(status));

            var request = new SetStateRequest() { EntityMoniker = target.ToEntityReference(), State = state, Status = status };
            service.Execute(request);
        }

        public static Entity ToEntity(this EntityReference reference)
        {
            _ = reference ?? throw new ArgumentNullException(nameof(reference));
                       
            return new Entity() { LogicalName = reference.LogicalName, Id = reference.Id };
        }

        public static T ToEntity<T>(this EntityReference reference) where T : Entity
        {
            _ = reference ?? throw new ArgumentNullException(nameof(reference));

            return reference.ToEntity().ToEntity<T>();
        }

        public static AttributeMetadata GetAttributeMetadata(this IOrganizationService service, string entityLogicalName, string attributeLogicalName)
        {
            _ = service ?? throw new ArgumentNullException(nameof(service));
            _ = entityLogicalName ?? throw new ArgumentNullException(nameof(entityLogicalName));
            _ = attributeLogicalName ?? throw new ArgumentNullException(nameof(attributeLogicalName));

            var request = new RetrieveAttributeRequest() { EntityLogicalName = entityLogicalName, LogicalName = attributeLogicalName };
            return (service.Execute(request) as RetrieveAttributeResponse).AttributeMetadata;
        }

        public static OptionMetadata GetOptionMetadata(this OptionSetValue value, IOrganizationService service, Entity entity, string attributeLogicalName)
        {
            _ = value ?? throw new ArgumentNullException(nameof(value));
            _ = service ?? throw new ArgumentNullException(nameof(service));
            _ = entity ?? throw new ArgumentNullException(nameof(entity));
            _ = attributeLogicalName ?? throw new ArgumentNullException(nameof(attributeLogicalName));

            var attributeMeta = service.GetAttributeMetadata(entity.LogicalName, attributeLogicalName);
            return attributeMeta is EnumAttributeMetadata metadata
                ? metadata.GetOptionMetadata(value.Value)
                : throw new InvalidCastException("The attribute is not an Enum type attribute");
        }

        public static OptionMetadata GetOptionMetadata(this EnumAttributeMetadata enumMeta, int value)
        {
            _ = enumMeta ?? throw new ArgumentNullException(nameof(enumMeta));
            
            return (from meta in enumMeta.OptionSet.Options where meta.Value == value select meta).FirstOrDefault();
        }

        public static string GetOptionSetText(this EnumAttributeMetadata enumMeta, int value)
        {
            _ = enumMeta ?? throw new ArgumentNullException(nameof(enumMeta));

            return enumMeta.GetOptionMetadata(value).GetOptionSetText();
        }

        public static string GetOptionSetText(this OptionSetValue value, IOrganizationService service, Entity entity, string attributeLogicalName)
        {
            _ = value ?? throw new ArgumentNullException(nameof(value));
            _ = service ?? throw new ArgumentNullException(nameof(service));
            _ = entity ?? throw new ArgumentNullException(nameof(entity));
            _ = attributeLogicalName ?? throw new ArgumentNullException(nameof(attributeLogicalName));

            var optionMeta = GetOptionMetadata(value, service, entity, attributeLogicalName);
            return optionMeta.GetOptionSetText();
        }

        public static string GetOptionSetText(this OptionMetadata optionMeta)
        {
            _ = optionMeta ?? throw new ArgumentNullException(nameof(optionMeta));
            if (optionMeta.Label != null && optionMeta.Label.UserLocalizedLabel != null) { return optionMeta.Label.UserLocalizedLabel.Label; }
            return string.Empty;
        }

        public static IList<T> ToList<T>(this EntityCollection entities) where T : Entity
        {
            _ = entities ?? throw new ArgumentNullException(nameof(entities));

            return entities.Entities.ToList<T>();
        }

        public static IList<T> ToList<T>(this IEnumerable<Entity> entities) where T : Entity
        {
            _ = entities ?? throw new ArgumentNullException(nameof(entities));

            return (from entity in entities select entity.ToEntity<T>()).ToList();
        }

    }
}

