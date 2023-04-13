using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

public class MeetingGroupMemberData
{
    public MeetingGroupMemberData(MeetingGroupId meetingGroupId, MemberId memberId)
    {
        MemberId = memberId;
        MeetingGroupId = meetingGroupId;
    }

    public MeetingGroupId MeetingGroupId { get; }

    public MemberId MemberId { get; }
}
