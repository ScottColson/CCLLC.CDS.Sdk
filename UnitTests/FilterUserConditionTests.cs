namespace CCLLC.CDS.Sdk.Tests
{
    using System;
    using CCLLC.CDS.Sdk;
    using CCLLC.CDS.Sdk.Tests.Builders;
    using DLaB.Xrm.Test;
    using DLaB.Xrm.Test.Builders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xrm.Sdk;
    using Shared.Proxies;

    [TestClass]
    public class FilterUserConditionTests
    {
        public class PluginUnderTest_SystemUser : TestPluginBase
        {
            public PluginUnderTest_SystemUser(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
            {
                RegisterUpdateHandler<Account>(ePluginStage.PreOperation, onUpdateHandler)
                    .ExecuteIf(filter => filter
                        .ExecutingUser().SystemUser());
            }   
        }

        public class PluginUnderTest_NotSystemUser : TestPluginBase
        {
            public PluginUnderTest_NotSystemUser(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
            {
                RegisterUpdateHandler<Account>(ePluginStage.PreOperation, onUpdateHandler)
                    .ExecuteIf(filter => filter
                        .Not().ExecutingUser().SystemUser());
            }
        }

        public class PluginUnderTest_TeamMember : TestPluginBase
        {
            public PluginUnderTest_TeamMember(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
            {
                RegisterUpdateHandler<Account>(ePluginStage.PreOperation, onUpdateHandler)
                    .ExecuteIf(filter => filter
                        .InitiatingUser().MemberOfTeam(TestCommon.Ids.Teams.Team2.EntityId));
            }
        }

        #region Filter_Should_ContinueWhenSystemUser

        [TestMethod]
        public void Filter_Should_ContinueWhenSystemUser()
        {
            new Test_Filter_Should_ContinueWhenSystemUser().Test();
        }

        private class Test_Filter_Should_ContinueWhenSystemUser : TestMethodClassBase
        {
            private struct TestData
            {
            }

            private struct Ids
            {
            }

            private struct CreatedIds
            {
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder()
                    .WithCommonData()
                    .WithEntities<Ids>()
                    .Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                // Arrange 
                service = new Builders.OrganizationServiceBuilder(service)
                    .Build();

                var target = new Account
                {
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithUser(TestCommon.Ids.Users.SystemUser)
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();

                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest_SystemUser(null, null).Execute(serviceProvider);
                }
                catch (Exception ex)
                {
                    pluginException = ex;
                }

                var modifiedTarget = serviceProvider.GetTarget<Account>();

                // Assert
                Assert.IsNull(pluginException);
                Assert.AreEqual("HandlerExecuted", modifiedTarget.Name);
            }
        }

        #endregion Filter_Should_ContinueWhenSystemUser

        #region Filter_Should_BypassWhenNotSystemUser

        [TestMethod]
        public void Filter_Should_BypassWhenNotSystemUser()
        {
            new Test_Filter_Should_BypassWhenNotSystemUser().Test();
        }

        private class Test_Filter_Should_BypassWhenNotSystemUser : TestMethodClassBase
        {
            private struct TestData
            {
            }

            private struct Ids
            {
            }

            private struct CreatedIds
            {
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder()
                    .WithCommonData()
                    .WithEntities<Ids>()
                    .Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                // Arrange 
                service = new Builders.OrganizationServiceBuilder(service)
                    .Build();

                var target = new Account
                {
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithUser(TestCommon.Ids.Users.User_1)
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();

                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest_SystemUser(null, null).Execute(serviceProvider);
                }
                catch (Exception ex)
                {
                    pluginException = ex;
                }

                var modifiedTarget = serviceProvider.GetTarget<Account>();

                // Assert
                Assert.IsNull(pluginException);
                Assert.IsNull(modifiedTarget.Name);
            }
        }

        #endregion Filter_Should_BypassWhenNotSystemUser

        #region Filter_Should_ContinueWhenNotSystemUser

        [TestMethod]
        public void Filter_Should_ContinueWhenNotSystemUser()
        {
            new Test_Filter_Should_ContinueWhenNotSystemUser().Test();
        }

        private class Test_Filter_Should_ContinueWhenNotSystemUser : TestMethodClassBase
        {
            private struct TestData
            {
            }

            private struct Ids
            {
            }

            private struct CreatedIds
            {
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder()
                    .WithCommonData()
                    .WithEntities<Ids>()
                    .Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                // Arrange 
                service = new Builders.OrganizationServiceBuilder(service)
                    .Build();

                var target = new Account
                {
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithUser(TestCommon.Ids.Users.User_1)
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();

                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest_NotSystemUser(null, null).Execute(serviceProvider);
                }
                catch (Exception ex)
                {
                    pluginException = ex;
                }

                var modifiedTarget = serviceProvider.GetTarget<Account>();

                // Assert
                Assert.IsNull(pluginException);
                Assert.AreEqual("HandlerExecuted", modifiedTarget.Name);
            }
        }

        #endregion Filter_Should_ContinueWhenNotSystemUser

        #region Filter_Should_ContinueWhenTeamMember

        [TestMethod]
        public void Filter_Should_ContinueWhenTeamMember()
        {
            new Test_Filter_Should_ContinueWhenTeamMember().Test();
        }

        private class Test_Filter_Should_ContinueWhenTeamMember : TestMethodClassBase
        {
            private struct TestData
            {
            }

            private struct Ids
            {
                public static readonly Id TeamMember = new Id<TeamMembership>("{89534093-BC56-42D1-A748-C0A3F42AD9CD}");
            }

            private struct CreatedIds
            {
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder()
                    .WithCommonData()
                    .WithBuilder<TeamMemberBuilder>(Ids.TeamMember, b => b
                        .WithUser(TestCommon.Ids.Users.User_2)
                        .WithTeam(TestCommon.Ids.Teams.Team2))
                    .WithEntities<Ids>()
                    .Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                // Arrange 
                service = new Builders.OrganizationServiceBuilder(service)
                    .Build();

                var target = new Account
                {
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithInitiatingUser(TestCommon.Ids.Users.User_2)
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();

                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest_TeamMember(null, null).Execute(serviceProvider);
                }
                catch (Exception ex)
                {
                    pluginException = ex;
                }

                var modifiedTarget = serviceProvider.GetTarget<Account>();

                // Assert
                Assert.IsNull(pluginException);
                Assert.AreEqual("HandlerExecuted", modifiedTarget.Name);
            }
        }

        #endregion Filter_Should_ContinueWhenTeamMember

    }
}
