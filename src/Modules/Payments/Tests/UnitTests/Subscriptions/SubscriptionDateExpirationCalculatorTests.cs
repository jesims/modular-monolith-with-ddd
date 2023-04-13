using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.Subscriptions;

[TestFixture]
public class SubscriptionDateExpirationCalculatorTests
{
    [Test]
    public void CalculateForNew_WhenPeriodMonthIsSelected_Test()
    {
        // Arrange
        var period = SubscriptionPeriod.Month;
        SystemClock.Set(new DateTime(2020, 5, 11));

        // Act
        var expirationDate = SubscriptionDateExpirationCalculator.CalculateForNew(period);

        // Assert
        Assert.That(expirationDate, Is.EqualTo(new DateTime(2020, 6, 11)));
    }

    [Test]
    public void CalculateForNew_WhenPeriodHalfYearIsSelected_Test()
    {
        // Arrange
        var period = SubscriptionPeriod.HalfYear;
        SystemClock.Set(new DateTime(2020, 5, 11));

        // Act
        var expirationDate = SubscriptionDateExpirationCalculator.CalculateForNew(period);

        // Assert
        Assert.That(expirationDate, Is.EqualTo(new DateTime(2020, 11, 11)));
    }

    [Test]
    public void
        CalculateForRenewal_WhenPeriodMonthIsSelected_AndExpireDateIsInTheFuture_ThenMonthsAreAddedToExpireDate()
    {
        // Arrange
        var period = SubscriptionPeriod.Month;
        SystemClock.Set(new DateTime(2020, 5, 11));
        var expirationDate = new DateTime(2020, 7, 1);

        // Act
        expirationDate = SubscriptionDateExpirationCalculator.CalculateForRenewal(expirationDate, period);

        // Assert
        Assert.That(expirationDate, Is.EqualTo(new DateTime(2020, 8, 1)));
    }

    [Test]
    public void CalculateForRenewal_WhenPeriodMonthIsSelected_AndExpireDatePassed_ThenMonthsAreAddedToNow()
    {
        // Arrange
        var period = SubscriptionPeriod.Month;
        SystemClock.Set(new DateTime(2020, 5, 11));
        var expirationDate = new DateTime(2020, 4, 1);

        // Act
        expirationDate = SubscriptionDateExpirationCalculator.CalculateForRenewal(expirationDate, period);

        // Assert
        Assert.That(expirationDate, Is.EqualTo(new DateTime(2020, 6, 11)));
    }
}
