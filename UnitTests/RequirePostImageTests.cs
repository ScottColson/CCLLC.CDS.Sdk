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
    public class RequirePostImageTests
    {
        public class PluginUnderTest : TestPluginBase
        {
            public PluginUnderTest(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
            {
                RegisterCreateHandler<Account>(ePluginStage.PostOperation, onCreateHandler)
                    .RequirePostImage(f => new { f.AccountNumber });
            }
        }

        #region RequirePostImage_Should_CauseExceptionWhenImageIsMissing

        [TestMethod]
        public void RequirePostImage_Should_CauseExceptionWhenImageIsMissing()
        {
            new Test_RequirePostImage_Should_CauseExceptionWhenImageIsMissing().Test();
        }

        private class Test_RequirePostImage_Should_CauseExceptionWhenImageIsMissing : TestMethodClassBase
        {
            private readonly InvalidPluginExecutionException ExpectedException = new InvalidPluginExecutionException("Unhandled Plugin Exception Event Registration Validation Exception. Plugin requires a post-image with field accountnumber for execution.");

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
                    .WithOutputParameter("id", Guid.NewGuid())
                    .WithRegisteredEvent(40, "Create")
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

        #endregion RequirePostImage_Should_CauseExceptionWhenImageIsMissing

        #region RequirePostImage_Should_CauseExceptionWhenFieldIsMissing

        [TestMethod]
        public void RequirePostImage_Should_CauseExceptionWhenFieldIsMissing()
        {
            new Test_RequirePostImage_Should_CauseExceptionWhenFieldIsMissing().Test();
        }        

        private class Test_RequirePostImage_Should_CauseExceptionWhenFieldIsMissing : TestMethodClassBase
        {
            private readonly InvalidPluginExecutionException ExpectedException = new InvalidPluginExecutionException("Unhandled Plugin Exception Event Registration Validation Exception. Plugin requires a post-image with field accountnumber for execution.");

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

                var postImage = new Account
                {
                    Address1Line1 = Guid.NewGuid().ToString()
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithPostImage(postImage)
                    .WithOutputParameter("id", Guid.NewGuid())
                    .WithRegisteredEvent(40, "Create")
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

        #endregion RequirePostImage_Should_CauseExceptionWhenFieldIsMissing

        #region RequirePostImage_Should_ContinueWhenFieldIsPresent

        [TestMethod]
        public void RequirePostImage_Should_ContinueWhenFieldIsPresent()
        {
            new Test_RequirePostImage_Should_ContinueWhenFieldIsPresent().Test();
        }

        private class Test_RequirePostImage_Should_ContinueWhenFieldIsPresent : TestMethodClassBase
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

                var postImage = new Account
                {
                    Address1Line1 = Guid.NewGuid().ToString(),
                    AccountNumber = Guid.NewGuid().ToString()
                };

                var pluginContext = new PluginExecutionContextBuilder()
                    .WithTarget(target)
                    .WithPostImage(postImage)
                    .WithOutputParameter("id", Guid.NewGuid())
                    .WithRegisteredEvent(40, "Create")
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

        #endregion RequirePostImage_Should_CauseExceptionWhenFieldIsMissing

    }
}
