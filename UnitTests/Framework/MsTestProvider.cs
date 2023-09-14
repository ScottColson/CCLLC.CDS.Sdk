namespace CCLLC.CDS.Sdk.Tests
{
    using System;
    using DLaB.Xrm.Test;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class MsTestProvider : ITestFrameworkProvider
    {
        public Type TestMethodAttributeType => typeof(TestMethodAttribute);

        public Exception GetFailedException(string message)
        {
            return new AssertFailedException(message);
        }

        public Exception GetInconclusiveException(string message)
        {
            return new AssertInconclusiveException(message);
        }
    }
}
