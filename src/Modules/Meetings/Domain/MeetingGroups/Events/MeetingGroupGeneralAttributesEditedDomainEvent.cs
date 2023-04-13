using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

public class MeetingGroupGeneralAttributesEditedDomainEvent : DomainEventBase
{
    public MeetingGroupGeneralAttributesEditedDomainEvent(string newName, string newDescription,
        MeetingGroupLocation newLocation)
    {
        NewName = newName;
        NewDescription = newDescription;
        NewLocation = newLocation;
    }

    public string NewName { get; }

    public string NewDescription { get; }

    public MeetingGroupLocation NewLocation { get; }
}
