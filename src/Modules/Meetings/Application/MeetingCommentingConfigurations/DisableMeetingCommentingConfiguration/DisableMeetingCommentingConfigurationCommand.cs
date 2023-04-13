using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.
    DisbaleMeetingCommentingConfiguration;



public class DisableMeetingCommentingConfigurationCommand : CommandBase
{
    public DisableMeetingCommentingConfigurationCommand(Guid meetingId)
    {
        MeetingId = meetingId;
    }

    public Guid MeetingId { get; }
}
