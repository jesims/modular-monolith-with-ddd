using System;
using System.Collections.Generic;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users.Events;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;

public class User : Entity, IAggregateRoot
{
    private readonly List<UserRole> _roles;
    private string _login;

    private string _password;

    private string _email;

    private bool _isActive;

    private string _firstName;

    private string _lastName;

    private string _name;

    private User()
    {
        // Only for EF.
    }

    private User(
        Guid id,
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        string name,
        UserRole role)
    {
        Id = new UserId(id);
        _login = login;
        _password = password;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _name = name;

        _isActive = true;

        _roles = new List<UserRole>();
        _roles.Add(role);

        AddDomainEvent(new UserCreatedDomainEvent(Id));
    }

    public UserId Id { get; }

    public static User CreateAdmin(
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        string name)
    {
        return new User(
            Guid.NewGuid(),
            login,
            password,
            email,
            firstName,
            lastName,
            name,
            UserRole.Administrator);
    }

    internal static User CreateFromUserRegistration(
        UserRegistrationId userRegistrationId,
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        string name)
    {
        return new User(
            userRegistrationId.Value,
            login,
            password,
            email,
            firstName,
            lastName,
            name,
            UserRole.Member);
    }
}
