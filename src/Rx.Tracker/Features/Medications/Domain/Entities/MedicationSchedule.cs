using System;
using System.Collections;
using System.Collections.Generic;

namespace Rx.Tracker.Features.Medications.Domain.Entities;

public class MedicationSchedule : IReadOnlyCollection<MedicationReminder>
{
    public IEnumerator<MedicationReminder> GetEnumerator() => throw new NotImplementedException();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count { get; }
}