using NodaTime;
using NodaTime.Extensions;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Queries;

public class LoadScheduleClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return DefaultScenario();
    }

    private static object[] DefaultScenario()
    {
        var now = DateTimeOffset.UnixEpoch.AddDays(5);
        var scheduledTime = now.ToOffsetDateTime();
        return CreateScenario(
            new MedicationScheduleFixture().WithEnumerable(
                [
                    new ScheduledMedicationFixture().WithScheduledTime(scheduledTime),
                    new ScheduledMedicationFixture().WithScheduledTime(scheduledTime.Plus(Duration.FromHours(2))),
                    new ScheduledMedicationFixture().WithScheduledTime(scheduledTime.Plus(Duration.FromHours(4))),
                    new ScheduledMedicationFixture().WithScheduledTime(scheduledTime.Plus(Duration.FromDays(2))),
                ]
            ).WithToday(scheduledTime.Date),
            now
        );
    }

    private static object[] CreateScenario(MedicationSchedule medicationSchedule, DateTimeOffset now) => [medicationSchedule, now];

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}