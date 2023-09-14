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
    public class TargetContainsConditionTests
    {
        public class PluginUnderTest_ContainsAny : TestPluginBase
        {
            public PluginUnderTest_ContainsAny(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
            {
                RegisterUpdateHandler<Account>(ePluginStage.PreOperation, onUpdateHandler)
                    .ExecuteIf(filter => filter
                        .ContainsAny(f => new { f.AccountNumber, f.Address1City }));
            }
        }

        public class PluginUnderTest_ContainsAll : TestPluginBase
        {
            public PluginUnderTest_ContainsAll(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
            {
                RegisterUpdateHandler<Account>(ePluginStage.PreOperation, onUpdateHandler)
                    .ExecuteIf(filter => filter
                        .ContainsAll(f => new { f.AccountNumber, f.Address1City }));
            }
        }

        public class PluginUnderTest_ChangedAny : TestPluginBase
        {
            public PluginUnderTest_ChangedAny(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
            {
                RegisterUpdateHandler<Account>(ePluginStage.PreOperation, onUpdateHandler)
                    .ExecuteIf(filter => filter
                        .ChangedAny(f => new { f.AccountNumber, f.Address1City }));
            }
        }


        #region Filter_Should_ContinueWhenTargetContainsAnyField

        [TestMethod]
        public void Filter_Should_ContinueWhenTargetContainsAnyField()
        {
            new Test_Filter_Should_ContinueWhenTargetContainsAnyField().Test();
        }

        private class Test_Filter_Should_ContinueWhenTargetContainsAnyField : TestMethodClassBase
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
                    AccountNumber = Guid.NewGuid().ToString()
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();

                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest_ContainsAny(null, null).Execute(serviceProvider);
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

        #endregion Filter_Should_ContinueWhenTargetContainsAnyField

        #region Filter_Should_BypassWhenTargetNotContainsField

        [TestMethod]
        public void Filter_Should_BypassWhenTargetNotContainsField()
        {
            new Test_Filter_Should_BypassWhenTargetNotContainsField().Test();
        }

        private class Test_Filter_Should_BypassWhenTargetNotContainsField : TestMethodClassBase
        {
            private struct TestData
            {
                public static readonly string AccountName = Guid.NewGuid().ToString();
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
                    Name = TestData.AccountName
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();

                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest_ContainsAny(null, null).Execute(serviceProvider);
                }
                catch (Exception ex)
                {
                    pluginException = ex;
                }

                var modifiedTarget = serviceProvider.GetTarget<Account>();

                // Assert
                Assert.IsNull(pluginException);
                Assert.AreEqual(TestData.AccountName, modifiedTarget.Name);
            }
        }

        #endregion Filter_Should_BypassWhenTargetNotContainsField

        #region Filter_Should_ContinueWhenTargetContainsAllFields

        [TestMethod]
        public void Filter_Should_ContinueWhenTargetContainsAllFields()
        {
            new Test_Filter_Should_ContinueWhenTargetContainsAllFields().Test();
        }

        private class Test_Filter_Should_ContinueWhenTargetContainsAllFields : TestMethodClassBase
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
                    AccountNumber = Guid.NewGuid().ToString(),
                    Address1City = Guid.NewGuid().ToString()
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();

                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest_ContainsAll(null, null).Execute(serviceProvider);
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

        #endregion Filter_Should_ContinueWhenTargetContainsAllFields

        #region Filter_Should_BypassWhenTargetNotContainsAllFields

        [TestMethod]
        public void Filter_Should_BypassWhenTargetNotContainsAllFields()
        {
            new Test_Filter_Should_BypassWhenTargetNotContainsAllFields().Test();
        }

        private class Test_Filter_Should_BypassWhenTargetNotContainsAllFields : TestMethodClassBase
        {
            private struct TestData
            {
                public static readonly string AccountName = Guid.NewGuid().ToString();
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
                    Name = TestData.AccountName,
                    AccountNumber = Guid.NewGuid().ToString()
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();

                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest_ContainsAll(null, null).Execute(serviceProvider);
                }
                catch (Exception ex)
                {
                    pluginException = ex;
                }

                var modifiedTarget = serviceProvider.GetTarget<Account>();

                // Assert
                Assert.IsNull(pluginException);
                Assert.AreEqual(TestData.AccountName, modifiedTarget.Name);
            }
        }

        #endregion Filter_Should_BypassWhenTargetNotContainsAllFields

        #region Filter_Should_ContinueWhenTargetChangesAnyField

        [TestMethod]
        public void Filter_Should_ContinueWhenTargetChangesAnyField()
        {
            new Test_Filter_Should_ContinueWhenTargetChangesAnyField().Test();
        }

        private class Test_Filter_Should_ContinueWhenTargetChangesAnyField : TestMethodClassBase
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

                var preImage = new Account
                {
                    AccountNumber = Guid.NewGuid().ToString(),
                    Address1City = Guid.NewGuid().ToString()
                };

                var target = new Account
                {
                    AccountNumber = Guid.NewGuid().ToString()
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithPreImage(preImage)
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();

                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest_ChangedAny(null, null).Execute(serviceProvider);
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

        #endregion Filter_Should_ContinueWhenTargetChangesAnyField
    }
}
