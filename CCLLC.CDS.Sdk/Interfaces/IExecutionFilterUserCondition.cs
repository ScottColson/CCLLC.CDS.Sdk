namespace CCLLC.CDS.Sdk
{
    using System;

    public interface IExecutionFilterUserCondition<TParent> : IExecutionFilterCondition where TParent : IExecutionFilter
    {
        TParent SystemUser();
        TParent EqualTo(params Guid[] userId);
        TParent EqualTo(params string[] userName);
        TParent MemberOfTeam(params Guid[] teamId);
        TParent MemberOfTeam(params string[] teamName);
        TParent HasRole(params string[] rollName);
    }
}
