using FluentAssertions;
using NodaTime;
using NodaTime.Extensions;
using Rx.Tracker.Extensions;
using System;

namespace Rx.Tracker.Tests.Extensions;

public class NodaTimeExtensionsTests
{
    [Theory]
    [InlineData(2025, 01, 06)]
    public void GivenADateTimeOffset_WhenIsInSameWeek_ThenShouldBeTrue(int year, int month, int day) =>
        // Given, When, Then
        new DateTimeOffset(new DateTime(year, month, day))
           .ToOffsetDateTime()
           .IsInSameWeek(new DateTime(year, month, day).AddDays(2).ToLocalDate())
           .Should()
           .BeTrue();

    [Fact]
    public void GivenADateTimeOffset_WhenIsInSameWeek_ThenShouldBeFalse() =>
        // Given, When, Then
        DateTimeOffset.UtcNow.ToOffsetDateTime().IsInSameWeek(LocalDate.MinIsoValue).Should().BeFalse();
}