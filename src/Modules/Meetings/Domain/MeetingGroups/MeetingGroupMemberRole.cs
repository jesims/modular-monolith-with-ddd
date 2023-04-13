using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

public class MeetingGroupMemberRole : ValueObject
{
    private MeetingGroupMemberRole(string value)
    {
        Value = value;
    }

    public static MeetingGroupMemberRole Organizer => new("Organizer");

    public static MeetingGroupMemberRole Member => new("Member");

    public string Value { get; }

    public static MeetingGroupMemberRole Of(string roleCode)
    {
        return new MeetingGroupMemberRole(roleCode);
    }
}
