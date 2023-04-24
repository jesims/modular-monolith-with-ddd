using System;
using System.Data;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members
{
    public class MembersQueryHelper
    {
        public static async Task<MemberDto> GetMember(MemberId memberId, IDbConnection connection)
        {
            return await connection.QuerySingleAsync<MemberDto>(
                "SELECT " +
                "member.id, " +
                "member.name, " +
                "member.login, " +
                "member.email " +
                "FROM sss_meetings.members AS member " +
                "WHERE member.id = @Id", new
                {
                    Id = memberId.Value
                });
        }

        public static async Task<MeetingGroupMemberData> GetMeetingGroupMember(MemberId memberId, MeetingId meetingOfGroupId, IDbConnection connection)
        {
            var result = await connection.QuerySingleAsync<MeetingGroupMemberResponse>(
                "SELECT " +
                $"meeting_group_member.meeting_group_id AS {nameof(MeetingGroupMemberResponse.MeetingGroupId)}, " +
                $"meeting_group_member.member_id AS {nameof(MeetingGroupMemberResponse.MemberId)} " +
                "FROM sss_meetings.meeting_group_members AS meeting_group_member " +
                "INNER JOIN sss_meetings.meetings AS meeting ON meeting.meeting_group_id = meeting_group_member.meeting_group_id " +
                "WHERE meeting_group_member.member_id = @MemberId AND meeting.id = @MeetingId",
                new
                {
                    MemberId = memberId.Value,
                    MeetingId = meetingOfGroupId.Value
                });

            return new MeetingGroupMemberData(
                new MeetingGroupId(result.MeetingGroupId),
                new MemberId(result.MemberId));
        }

        private class MeetingGroupMemberResponse
        {
            public Guid MeetingGroupId { get; set; }

            public Guid MemberId { get; set; }
        }
    }
}