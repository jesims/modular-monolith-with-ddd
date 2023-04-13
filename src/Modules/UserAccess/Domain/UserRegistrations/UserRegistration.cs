using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Events;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Rules;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;

public class UserRegistration : Entity, IAggregateRoot
{
    private readonly string _login;

    private readonly string _password;

    private readonly string _email;

    private readonly string _firstName;

    private readonly string _lastName;

    private readonly string _name;

    private readonly DateTime _registerDate;

    private UserRegistrationStatus _status;

    private DateTime? _confirmedDate;

    private UserRegistration()
    {
        // Only EF.
    }

    private UserRegistration(
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        IUsersCounter usersCounter,
        string confirmLink)
    {
        CheckRule(new UserLoginMustBeUniqueRule(usersCounter, login));

        Id = new UserRegistrationId(Guid.NewGuid());
        _login = login;
        _password = password;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _name = $"{firstName} {lastName}";
        _registerDate = DateTime.UtcNow;
        _status = UserRegistrationStatus.WaitingForConfirmation;

        AddDomainEvent(new NewUserRegisteredDomainEvent(
            Id,
            _login,
            _email,
            _firstName,
            _lastName,
            _name,
            _registerDate,
            confirmLink));
    }

    public UserRegistrationId Id { get; }

    public static UserRegistration RegisterNewUser(
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        IUsersCounter usersCounter,
        string confirmLink)
    {
        return new UserRegistration(login, password, email, firstName, lastName, usersCounter, confirmLink);
    }

    public User CreateUser()
    {
        CheckRule(new UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(_status));

        return User.CreateFromUserRegistration(
            Id,
            _login,
            _password,
            _email,
            _firstName,
            _lastName,
            _name);
    }

    public void Confirm()
    {
        CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
        CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(_status));

        _status = UserRegistrationStatus.Confirmed;
        _confirmedDate = DateTime.UtcNow;

        AddDomainEvent(new UserRegistrationConfirmedDomainEvent(Id));
    }

    public void Expire()
    {
        CheckRule(new UserRegistrationCannotBeExpiredMoreThanOnceRule(_status));

        _status = UserRegistrationStatus.Expired;

        AddDomainEvent(new UserRegistrationExpiredDomainEvent(Id));
    }
}
