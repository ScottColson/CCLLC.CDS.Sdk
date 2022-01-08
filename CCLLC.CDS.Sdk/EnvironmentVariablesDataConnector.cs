namespace CCLLC.CDS.Sdk
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using CCLLC.Core;

    /// <summary>
    /// Provides access to settings stored in CDS Environment Variable entities. Returns current
    /// values as key/value dictionary.
    /// </summary>
    public class EnvironmentVariablesDataConnector : ISettingsProviderDataConnector
    {
        public EnvironmentVariablesDataConnector() { }

        public IReadOnlyDictionary<string, string> LoadSettings(IDataService dataService)
        {
            var orgService = dataService.ToOrgService();

            var definitions = GetVariableDefinitions(dataService);
            var overrideValues = GetVariableValues(dataService);

            // Build a dictionary with entires keyed by display name and schema name.
            Dictionary<string, string> entries = new Dictionary<string, string>();

            foreach (var definition in definitions)
            {
                var key1 = definition.GetAttributeValue<string>("displayname");
                var key2 = definition.GetAttributeValue<string>("schemaname");

                // Use override value if present otherwise default value from definition.
                var value = overrideValues
                    .Where(v => v.GetAttributeValue<EntityReference>("environmentvariabledefinitionid")?.Id == definition.Id)
                    .FirstOrDefault()?.GetAttributeValue<string>("value") ?? definition.GetAttributeValue<string>("defaultvalue");

                // Only include in the returned data if value is not null or empty.
                if (!string.IsNullOrEmpty(value))
                {
                    entries.Add(key1, value);
                    entries.Add(key2, value);
                }
            }

            return entries;
        }

        private IList<Entity> GetVariableDefinitions(IDataService dataService)
        {
            var orgService = dataService.ToOrgService();

            var qry = new QueryByAttribute("environmentvariabledefinition");
            qry.AddAttributeValue("statecode", 0);
            qry.ColumnSet = new ColumnSet("environmentvariabledefinitionid", "displayname", "schemaname", "defaultvalue");

            var records = orgService.RetrieveMultiple(qry).Entities.ToList();

            return records;

        }

        private IList<Entity> GetVariableValues(IDataService dataService)
        {
            var orgService = dataService.ToOrgService();

            var qry = new QueryByAttribute("environmentvariablevalue");
            qry.AddAttributeValue("statecode", 0);
            qry.ColumnSet = new ColumnSet("environmentvariabledefinitionid", "value");

            var records = orgService.RetrieveMultiple(qry).Entities.ToList();

            return records;

        }
    }
}
