using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.SetMeetingGroupExpirationDate;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Policies;
using Dapper;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions
{
    public class MemberSubscriptionExpirationDateChangedNotificationHandler :
        INotificationHandler<MemberSubscriptionExpirationDateChangedNotification>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly ICommandsScheduler _commandsScheduler;

        public MemberSubscriptionExpirationDateChangedNotificationHandler(ISqlConnectionFactory sqlConnectionFactory, ICommandsScheduler commandsScheduler)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(MemberSubscriptionExpirationDateChangedNotification notification, CancellationToken cancellationToken)
        {
            var sql = "SELECT " +
                      $"meeting_group_member.meeting_group_id AS [{nameof(MeetingGroupMemberResponse.MeetingGroupId)}], " +
                      $"meeting_group_member.role_code AS [{nameof(MeetingGroupMemberResponse.RoleCode)}] " +
                      "FROM sss_meetings.meeting_group_members AS meeting_group_member " +
                      "WHERE meeting_group_member.member_id = @MemberId";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            var meetingGroupMembers = await connection.QueryAsync<MeetingGroupMemberResponse>(
                sql,
                new
                {
                    MemberId = notification.DomainEvent.MemberId.Value
                });

            var meetingGroupList = meetingGroupMembers.AsList();

            List<MeetingGroupMemberData> meetingGroups = meetingGroupList
                .Select(x =>
                    new MeetingGroupMemberData(
                        new MeetingGroupId(x.MeetingGroupId),
                        MeetingGroupMemberRole.Of(x.RoleCode)))
                .ToList();

            var meetingGroupsCoveredByMemberSubscription =
                MeetingGroupExpirationDatePolicy.GetMeetingGroupsCoveredByMemberSubscription(meetingGroups);

            foreach (var meetingGroup in meetingGroupsCoveredByMemberSubscription)
            {
                await _commandsScheduler.EnqueueAsync(new SetMeetingGroupExpirationDateCommand(
                    Guid.NewGuid(),
                    meetingGroup.Value,
                    notification.DomainEvent.ExpirationDate));
            }
        }

        private class MeetingGroupMemberResponse
        {
            public Guid MeetingGroupId { get; set; }

            public string RoleCode { get; set; }
        }
    }
}