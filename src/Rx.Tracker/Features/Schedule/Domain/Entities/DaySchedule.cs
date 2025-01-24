using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using NodaTime;
using ReactiveMarbles.Extensions;

namespace Rx.Tracker.Features.Schedule.Domain.Entities;

public class DaySchedule : DisposableObject, IReadOnlyDictionary<IsoDayOfWeek, IReadOnlyList<ScheduledMedication>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DaySchedule"/> class.
    /// </summary>
    /// <param name="group">the grouping.</param>
    public DaySchedule(IGroup<ScheduledMedication, Id, OffsetDateTime> group)
    {
        Day = group.Key;
        group.Cache.Connect()
           .ToCollection()
           .Select(scheduledMedications => scheduledMedications.Where(scheduledMedication => scheduledMedication.ScheduledTime.DayOfWeek == Day.DayOfWeek))
           .Subscribe(medication => Medication = medication.ToList())
           .DisposeWith(Garbage);
    }

    public IReadOnlyList<ScheduledMedication> Medication { get; set; } = [];

    public OffsetDateTime Day { get; set; }

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<IsoDayOfWeek, IReadOnlyList<ScheduledMedication>>> GetEnumerator() => _week.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc/>
    public int Count => _week.Count;

    /// <inheritdoc/>
    public bool ContainsKey(IsoDayOfWeek key) => _week.ContainsKey(key);

    /// <inheritdoc/>
    public bool TryGetValue(IsoDayOfWeek key, [MaybeNullWhen(false)] out IReadOnlyList<ScheduledMedication> value) => _week.TryGetValue(key, out value);

    /// <inheritdoc/>
    public IReadOnlyList<ScheduledMedication> this[IsoDayOfWeek key] => _week[key];

    /// <inheritdoc/>
    public IEnumerable<IsoDayOfWeek> Keys => _week.Keys;

    /// <inheritdoc/>
    public IEnumerable<IReadOnlyList<ScheduledMedication>> Values => _week.Values;

    private IReadOnlyDictionary<IsoDayOfWeek, IReadOnlyList<ScheduledMedication>> _week = new Dictionary<IsoDayOfWeek, IReadOnlyList<ScheduledMedication>>();
}