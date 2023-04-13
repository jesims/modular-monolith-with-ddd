using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.
    EnableMeetingCommentingConfiguration;



public class EnableMeetingCommentingConfigurationCommand : CommandBase
{
    public EnableMeetingCommentingConfigurationCommand(Guid meetingId)
    {
        MeetingId = meetingId;
    }

    public Guid MeetingId { get; }
}
