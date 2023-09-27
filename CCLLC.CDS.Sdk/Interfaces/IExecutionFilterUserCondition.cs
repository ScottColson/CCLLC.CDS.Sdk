namespace CCLLC.CDS.Sdk
{
    using System;

    public interface IExecutionFilterUserCondition<TParent> : IExecutionFilterCondition where TParent : IExecutionFilter
    {
        /// <summary>
        /// Tests whether user is the SYSTEM user.
        /// </summary>
        /// <returns></returns>
        TParent SystemUser();

        /// <summary>
        /// Tests whether the user is one of the specified users based on record id or full name of the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        TParent EqualTo(params Guid[] userId);

        /// <summary>
        /// Tests whether the user is one of the specified users based on record id or full name of the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        TParent EqualTo(params string[] userName);

        /// <summary>
        /// Tests whether the user is a member of any of the specified teams based on record id or team name.
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        TParent MemberOfTeam(params Guid[] teamId);

        /// <summary>
        /// Tests whether the user is a member of any of the specified teams based on record id or team name.
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        TParent MemberOfTeam(params string[] teamName);

        /// <summary>
        /// Tests wether the user has one of the specified security roles directly or via team membership.
        /// </summary>
        /// <param name="rollName"></param>
        /// <returns></returns>
        TParent HasRole(params string[] rollName);
    }
}
