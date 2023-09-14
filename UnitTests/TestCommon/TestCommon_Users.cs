namespace CCLLC.CDS.Sdk.Tests
{
    using DLaB.Xrm.Test;
    using CCLLC.CDS.Sdk.Tests.Builders;
    using Shared.Proxies;

    public static partial class TestCommon
    {
        public partial struct Ids
        {
            public partial struct Users
            {
                public static readonly Id User_1 = new Id<SystemUser>("{D91FA010-193F-42F3-B0AB-53F9867B0E75}");
                public static readonly Id User_2 = new Id<SystemUser>("{66E24943-DD1B-4C74-9DC0-2F4ED505A8D5}");
                public static readonly Id User_3 = new Id<SystemUser>("{83BEE8D5-B7F6-41AE-835C-4E5EF3C4D90F}");
                public static readonly Id SystemUser = new Id<SystemUser>("{A75C2064-CEAB-4D35-A1EA-41091B057FA9}");
            }
        }
        

        private static CrmEnvironmentBuilder WithUsers(this CrmEnvironmentBuilder builder)
        {
            builder
                .WithBuilder<SystemUserBuilder>(Ids.Users.User_1, b => b
                    .WithName("User 1"))
                .WithBuilder<SystemUserBuilder>(Ids.Users.User_2, b => b
                    .WithName("User 2"))
                .WithBuilder<SystemUserBuilder>(Ids.Users.User_3, b => b
                    .WithName("User 3"))
                .WithBuilder<SystemUserBuilder>(Ids.Users.SystemUser, b => b
                    .WithName("SYSTEM"));                

            return builder;
        }
    }
}
