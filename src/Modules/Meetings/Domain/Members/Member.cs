using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

public class Member : Entity, IAggregateRoot
{
    private string _login;

    private string _email;

    private string _firstName;

    private string _lastName;

    private string _name;

    private DateTime _createDate;

    private Member()
    {
        // Only for EF.
    }

    private Member(Guid id, string login, string email, string firstName, string lastName, string name)
    {
        Id = new MemberId(id);
        _login = login;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _name = name;
        _createDate = SystemClock.Now;

        AddDomainEvent(new MemberCreatedDomainEvent(Id));
    }

    public MemberId Id { get; }

    public static Member Create(Guid id, string login, string email, string firstName, string lastName, string name)
    {
        return new Member(id, login, email, firstName, lastName, name);
    }
}
