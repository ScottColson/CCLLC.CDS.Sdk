namespace CCLLC.CDS.Sdk.Tests
{
    using System;
    using CCLLC.CDS.Sdk;
    using CCLLC.CDS.Sdk.Tests.Builders;
    using DLaB.Xrm.Test.Builders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xrm.Sdk;
    using Shared.Proxies;

    [TestClass]
    public class RequirePreImageTests
    {
        public class PluginUnderTest : TestPluginBase
        {
            public PluginUnderTest(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
            {
                RegisterUpdateHandler<Account>(ePluginStage.PreOperation, onUpdateHandler)
                    .RequirePreImage(f => new { f.AccountNumber });
            }
        }

        #region RequirePreImage_Should_CauseExceptionWhenImageIsMissing

        [TestMethod]
        public void RequirePreImage_Should_CauseExceptionWhenImageIsMissing()
        {
            new Test_RequirePreImage_Should_CauseExceptionWhenImageIsMissing().Test();
        }

        private class Test_RequirePreImage_Should_CauseExceptionWhenImageIsMissing : TestMethodClassBase
        {
            private readonly InvalidPluginExecutionException ExpectedException = new InvalidPluginExecutionException("Unhandled Plugin Exception Event Registration Validation Exception. Plugin requires a pre-image with field accountnumber for execution.");

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
                    .WithRegisteredEvent(20, "Update")
                    .Build();

                var serviceProvider = new Builders.ServiceProviderBuilder(service, pluginContext, new DebugLogger())
                    .Build();


                // Test
                Exception pluginException = null;
                try
                {
                    new PluginUnderTest(null, null).Execute(serviceProvider);
                }
                catch(Exception ex)
                {
                    pluginException = ex;
                }

                // Assert
                Assert.IsNotNull(pluginException);
                Assert.AreEqual(ExpectedException.Message, pluginException.Message);
            }
        }

        #endregion RequirePreImage_Should_CauseExceptionWhenImageIsMissing

        #region RequirePreImage_Should_CauseExceptionWhenFieldIsMissing

        [TestMethod]
        public void RequirePreImage_Should_CauseExceptionWhenFieldIsMissing()
        {
            new Test_RequirePreImage_Should_CauseExceptionWhenFieldIsMissing().Test();
        }        

        private class Test_RequirePreImage_Should_CauseExceptionWhenFieldIsMissing : TestMethodClassBase
        {
            private readonly InvalidPluginExecutionException ExpectedException = new InvalidPluginExecutionException("Unhandled Plugin Exception Event Registration Validation Exception. Plugin requires a pre-image with field accountnumber for execution.");

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

                var preImage = new Account
                {
                    Address1Line1 = Guid.NewGuid().ToString()
                };

                // Post create context with post image missing required field
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
                    new PluginUnderTest(null, null).Execute(serviceProvider);
                }
                catch (Exception ex)
                {
                    pluginException = ex;
                }

                // Assert
                Assert.IsNotNull(pluginException);
                Assert.AreEqual(ExpectedException.Message, pluginException.Message);
            }
        }

        #endregion RequirePreImage_Should_CauseExceptionWhenFieldIsMissing

        #region RequirePreImage_Should_ContinueWhenFieldIsPresent

        [TestMethod]
        public void RequirePreImage_Should_ContinueWhenFieldIsPresent()
        {
            new Test_RequirePreImage_Should_ContinueWhenFieldIsPresent().Test();
        }

        private class Test_RequirePreImage_Should_ContinueWhenFieldIsPresent : TestMethodClassBase
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

                var preImage = new Account
                {
                    Address1Line1 = Guid.NewGuid().ToString(),
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
                    new PluginUnderTest(null, null).Execute(serviceProvider);
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

        #endregion RequirePreImage_Should_CauseExceptionWhenFieldIsMissing

    }
}
