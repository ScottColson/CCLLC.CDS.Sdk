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
    public class FieldValueConditionTests
    {
        public class PluginUnderTest_HasStatus : TestPluginBase
        {
            public PluginUnderTest_HasStatus(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
            {
                RegisterUpdateHandler<Account>(ePluginStage.PreOperation, onUpdateHandler)
                    .ExecuteIf(filter => filter
                        .IsActive()
                        .HasStatus(Account.eStatusCode.Active)
                        .IncomingValue(Account.Fields.Name).IsEqualTo("Testname")
                        .IncomingValue(Account.Fields.Name).IsLike("Test*")
                        .IncomingValue(Account.Fields.Name).IsLike("*Name")
                        .IncomingValue(Account.Fields.CreditOnHold).IsEqualTo(true)
                        .IncomingValue(Account.Fields.CreditLimit).IsEqualTo(10000)
                        .IncomingValue(Account.Fields.LastOnHoldTime).IsEqualTo(new DateTime(2020,1,1))
                        .IncomingValue(Account.Fields.CreditLimit).IsGreaterThan(5000.01M)
                        .IncomingValue(Account.Fields.CreditLimit).IsLessThan(10001)
                        .IncomingValue(Account.Fields.NumberOfEmployees).IsGreaterThan(2)
                        .IncomingValue(Account.Fields.NumberOfEmployees).IsLessThan(8));
            }
        }

        #region Filter_Should_ContinueWhenTargetFieldValueTestsPass

        [TestMethod]
        public void Filter_Should_ContinueWhenTargetFieldValueTestsPass()
        {
            new Test_Filter_Should_ContinueWhenTargetFieldValueTestsPass().Test();
        }

        private class Test_Filter_Should_ContinueWhenTargetFieldValueTestsPass : TestMethodClassBase
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
                    Name = "TestName",
                    StateCode = Account.eStateCode.Active,
                    StatusCode = Account.eStatusCode.Active,
                    CreditOnHold = true,
                    CreditLimit = 10000,
                    LastOnHoldTime = new DateTime(2020,1,1),
                    NumberOfEmployees = 6                    
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
                    new PluginUnderTest_HasStatus(null, null).Execute(serviceProvider);
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

        #endregion Filter_Should_ContinueWhenTargetFieldValueTestsPass

    }
}
