using Microsoft.Xrm.Sdk;
using System;

namespace CCLLC.CDS.Sdk
{
    /// <summary>
    /// Extension methods for the OrganizationRequest and OrgnaizationResponse classes.
    /// </summary>
    public static class OrganizationExtensions
    {
        public static T To<T>(this OrganizationResponse response) where T : OrganizationResponse, new()
        {
            _ = response ?? throw new ArgumentNullException(nameof(response));

            return new T()
            {
                Results = response.Results
            };
        }

        public static T To<T>(this OrganizationRequest request) where T : OrganizationRequest, new()
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            return new T()
            {
                Parameters = request.Parameters
            };
        }
    }
}
