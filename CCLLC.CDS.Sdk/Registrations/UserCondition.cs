namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xrm.Sdk.Query;

    public class ExecutingUserCondition<TParent> : UserCondition<TParent> where TParent : IExecutionFilter
    {
        public ExecutingUserCondition(TParent parent)
            : base(parent, true)
        {
        }
    }

    public class InitiatingUserCondition<TParent> : UserCondition<TParent> where TParent : IExecutionFilter
    {
        public InitiatingUserCondition(TParent parent) :
            base(parent, false)
        {
        }
    }

    public abstract class UserCondition<TParent> : ExecutionFilterCondition, IExecutionFilterCondition, IExecutionFilterUserCondition<TParent> where TParent : IExecutionFilter
    {
        private bool isExecutingUserCondition = false;
        private TParent parent;
        private List<Guid> requiredUserIds = null;
        private List<string> requiredUserNames = null;
        private List<Guid> requiredTeamIds = null;
        private List<string> requiredTeamNames = null;
        private List<string> requiredRoleNames = null;

        public UserCondition(TParent parent, bool useExecutingUser)
        {
            this.parent = parent;
            isExecutingUserCondition = useExecutingUser;
        }

        TParent IExecutionFilterUserCondition<TParent>.EqualTo(params Guid[] userId)
        {
            requiredUserIds = new List<Guid>(userId);
            return parent;
        }

        TParent IExecutionFilterUserCondition<TParent>.EqualTo(params string[] userName)
        {
            requiredUserNames = new List<string>(userName);
            return parent;
        }

        TParent IExecutionFilterUserCondition<TParent>.HasRole(params string[] rollName)
        {
            requiredRoleNames = new List<string>(rollName);
            return parent;
        }

        TParent IExecutionFilterUserCondition<TParent>.MemberOfTeam(params Guid[] teamId)
        {
            requiredTeamIds = new List<Guid>(teamId);
            return parent;
        }

        TParent IExecutionFilterUserCondition<TParent>.MemberOfTeam(params string[] teamName)
        {
            requiredTeamNames = new List<string>(teamName);
            return parent;
        }

        TParent IExecutionFilterUserCondition<TParent>.SystemUser()
        {
            requiredUserNames = new List<string>();
            requiredUserNames.Add("SYSTEM");
            return parent;
        }

        override public bool TestCondition(ICDSPluginExecutionContext executionContext)
        {            
            Guid userId = isExecutingUserCondition ? executionContext.UserId : executionContext.InitiatingUserId;

            bool result = TestUserIds(executionContext, userId)
                || TestUserNames(executionContext, userId)
                || TestTeamMembershipById(executionContext, userId)
                || TestTeamMembershipByName(executionContext, userId)
                || TestRoles(executionContext, userId);

            return result;
        }

        private bool TestUserIds(ICDSPluginExecutionContext executionContext, Guid userId)
        {
            if (requiredUserIds?.Contains(userId) == true)
            {
                return true;
            }

            return false;
        }

        private bool TestUserNames(ICDSExecutionContext executionContext, Guid userId)
        {
            if (requiredUserNames is null)
            {
                return false;
            }

            var qryUsers = new QueryExpression
            {
                EntityName = "systemuser",
                ColumnSet = new ColumnSet(new string[] { "fullname" }),
                Criteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression("systemuserid", ConditionOperator.Equal, userId)
                    }
                }
            };

            var users = executionContext.ElevatedOrganizationService.RetrieveMultiple(qryUsers).Entities;

            if (users.Count != 1)
            {
                return false;
            }

            foreach(var name in requiredUserNames)
            {
                if (users[0].GetAttributeValue<string>("fullname").ToLower() == name.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    
        private bool TestTeamMembershipById(ICDSExecutionContext executionContext, Guid userId)
        {
            if (requiredTeamIds is null)
            {
                return false;
            }

            return executionContext.Security.IsUserTeamMember(userId, requiredTeamIds.ToArray());
        }
    
        private bool TestTeamMembershipByName(ICDSExecutionContext executionContext, Guid userId)
        {
            if (requiredTeamNames is null)
            {
                return false;
            }

            return executionContext.Security.IsUserTeamMember(userId, requiredTeamNames.ToArray());
        }

        private bool TestRoles(ICDSPluginExecutionContext executionContext, Guid userId)
        {
            if (requiredRoleNames is null)
            {
                return false;
            }

            return executionContext.Security.IsUserAssignedRole(userId, requiredRoleNames.ToArray());
        }
    }
}
