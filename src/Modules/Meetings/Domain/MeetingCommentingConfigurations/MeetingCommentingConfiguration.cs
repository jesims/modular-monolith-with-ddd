using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;

public class MeetingCommentingConfiguration : Entity, IAggregateRoot
{
    private readonly MeetingId _meetingId;

    private bool _isCommentingEnabled;

    private MeetingCommentingConfiguration(MeetingId meetingId)
    {
        Id = new MeetingCommentingConfigurationId(Guid.NewGuid());
        _meetingId = meetingId;
        _isCommentingEnabled = true;

        AddDomainEvent(new MeetingCommentingConfigurationCreatedDomainEvent(_meetingId, _isCommentingEnabled));
    }

    private MeetingCommentingConfiguration()
    {
        // Only for EF.
    }

    public MeetingCommentingConfigurationId Id { get; }

    public void EnableCommenting(MemberId enablingMemberId, MeetingGroup meetingGroup)
    {
        CheckRule(new MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule(enablingMemberId, meetingGroup));

        if (!_isCommentingEnabled)
        {
            _isCommentingEnabled = true;
            AddDomainEvent(new MeetingCommentingEnabledDomainEvent(_meetingId));
        }
    }

    public void DisableCommenting(MemberId disablingMemberId, MeetingGroup meetingGroup)
    {
        CheckRule(new MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule(disablingMemberId, meetingGroup));

        if (_isCommentingEnabled)
        {
            _isCommentingEnabled = false;
            AddDomainEvent(new MeetingCommentingDisabledDomainEvent(_meetingId));
        }
    }

    public bool GetIsCommentingEnabled()
    {
        return _isCommentingEnabled;
    }

    internal static MeetingCommentingConfiguration Create(MeetingId meetingId)
    {
        return new MeetingCommentingConfiguration(meetingId);
    }
}
