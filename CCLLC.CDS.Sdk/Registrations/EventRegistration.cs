namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Microsoft.Xrm.Sdk;

    public abstract class EventRegistration : IPluginEventRegistration 
    {
        class ImageRequirement
        {

            public ImageRequirement() { }

            public ImageRequirement(string fieldName)
            {
                RequiredField = fieldName;
            }

            public string RequiredField { get; }

            public bool Test(Entity image)
            {
                if (image is null)
                {
                    return false;
                }

                if (RequiredField is null)
                {
                    return true;
                }

                if (!image.Contains(RequiredField))
                {
                    return false;
                }

                return true;
            }
        }

        private IList<ImageRequirement> PreImageRequirements { get; } = new List<ImageRequirement>();
        private IList<ImageRequirement> PostImageRequirements { get; } = new List<ImageRequirement>();
        private IList<IExecutionFilter> ExecutionContextFilters { get; } = new List<IExecutionFilter>();


        private string handlerId;
        /// <summary>
        /// Identifying name for the handler. Used in logging events.
        /// </summary>
        public string HandlerId
        {
            get { return handlerId ?? string.Empty; }
            set { handlerId = value; }
        }

        /// <summary>
        /// Execution pipeline stage that the plugin should be registered against.
        /// </summary>
        public ePluginStage Stage { get; set; }
        /// <summary>
        /// Logical name of the entity that the plugin should be registered against. Leave 'null' to register against all entities.
        /// </summary>
        public string EntityName { get; }
        /// <summary>
        /// Name of the message that the plugin should be triggered off of.
        /// </summary>
        public string MessageName { get; }

        public EventRegistration(string entityName, string messageName)
        {
            EntityName = entityName;
            MessageName = messageName;
        }

        protected abstract void InvokeRegistration(ICDSPluginExecutionContext executionContext);

        public void Invoke(ICDSPluginExecutionContext executionContext)
        {
            ValidatePreImageRequirements(executionContext.PreImage);
            ValidatePostImageRequirements(executionContext.PostImage);

            if (ShouldExecute(executionContext))
            {
                InvokeRegistration(executionContext);
            }
        }

        protected void AddExecutionFilter(IExecutionFilter executionFilter)
        {
            ExecutionContextFilters.Add(executionFilter);
        }

        public void AddPreImageRequirement(params string[] fieldNames)
        {
            if (fieldNames.Length == 0)
            {
                PreImageRequirements.Add(new ImageRequirement());
            }
            else
            {
                foreach (string field in fieldNames)
                {
                    PreImageRequirements.Add(new ImageRequirement(field));
                }
            }
        }

        public void AddPreImageRequirement<TEntity>(Expression<Func<TEntity, object>> anonymousTypeInitializer) where TEntity : Entity
        {
            var fields = anonymousTypeInitializer.GetAttributeNamesArray();
            AddPreImageRequirement(fields);
        }

        public void AddPostImageRequirement(params string[] fieldNames)
        {
            if (fieldNames.Length == 0)
            {
                PostImageRequirements.Add(new ImageRequirement());
            }
            else
            {
                foreach (string field in fieldNames)
                {
                    PostImageRequirements.Add(new ImageRequirement(field));
                }
            }
        }

        public void AddPostImageRequirement<TEntity>(Expression<Func<TEntity, object>> anonymousTypeInitializer) where TEntity : Entity
        {
            var fields = anonymousTypeInitializer.GetAttributeNamesArray();
            AddPostImageRequirement(fields);
        }

        private void ValidatePreImageRequirements(Entity image)
        {
            foreach (var requirement in PreImageRequirements)
            {
                if (false == requirement.Test(image))
                {
                    if (requirement.RequiredField is null)
                    {
                        throw new Exception($"Event Registration Validation Exception. Plugin requires a pre-image for execution.");
                    }
                    else
                    {
                        throw new Exception($"Event Registration Validation Exception. Plugin requires a pre-image with field {requirement.RequiredField} for execution.");
                    }
                }
            }
        }

        private void ValidatePostImageRequirements(Entity image)
        {
            foreach (var requirement in PostImageRequirements)
            {
                if (false == requirement.Test(image))
                {
                    if (requirement.RequiredField is null)
                    {
                        throw new Exception($"Event Registration Validation Exception. Plugin requires a post-image for execution.");
                    }
                    else
                    {
                        throw new Exception($"Event Registration Validation Exception. Plugin requires a post-image with field {requirement.RequiredField} for execution.");
                    }
                }
            }
        }

        private bool ShouldExecute(ICDSPluginExecutionContext executionContext)
        {
            if (ExecutionContextFilters.Count <= 0)
            {
                return true;
            }

            foreach (var filter in ExecutionContextFilters)
            {
                if (filter.Test(executionContext))
                {
                    return true;
                }
            }

            return false;
        }

       
    }
}
