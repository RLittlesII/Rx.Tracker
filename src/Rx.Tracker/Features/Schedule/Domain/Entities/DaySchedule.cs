using DynamicData;
using NodaTime;
using ReactiveMarbles.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;

namespace Rx.Tracker.Features.Schedule.Domain.Entities;

/// <summary>
/// Represents the schedule of medication for a given date.
/// </summary>
public class DaySchedule : DisposableObject, IReadOnlyDictionary<IsoDayOfWeek, IReadOnlyList<ScheduledMedication>>
{
    /// <inheritdoc />
    public IEnumerator<KeyValuePair<IsoDayOfWeek, IReadOnlyList<ScheduledMedication>>> GetEnumerator() => _week.GetEnumerator();

    /// <inheritdoc />
    public bool ContainsKey(IsoDayOfWeek key) => _week.ContainsKey(key);

    /// <inheritdoc />
    public bool TryGetValue(IsoDayOfWeek key, [MaybeNullWhen(false)] out IReadOnlyList<ScheduledMedication> value) => _week.TryGetValue(key, out value);

    /// <inheritdoc />
    public IReadOnlyList<ScheduledMedication> this[IsoDayOfWeek key] => _week[key];

    /// <summary>
    /// Gets the <see cref="ScheduledMedication" /> for the day.
    /// </summary>
    public IReadOnlyList<ScheduledMedication> Medication { get; private set; } = [];

    /// <summary>
    /// Gets the <see cref="OffsetDateTime" /> for the schedule.
    /// </summary>
    public OffsetDateTime Day { get; }

    /// <inheritdoc />
    public int Count => _week.Count;

    /// <inheritdoc />
    public IEnumerable<IsoDayOfWeek> Keys => _week.Keys;

    /// <inheritdoc />
    public IEnumerable<IReadOnlyList<ScheduledMedication>> Values => _week.Values;

    /// <summary>
    /// Initializes a new instance of the <see cref="DaySchedule" /> class.
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

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private readonly IReadOnlyDictionary<IsoDayOfWeek, IReadOnlyList<ScheduledMedication>> _week =
        new Dictionary<IsoDayOfWeek, IReadOnlyList<ScheduledMedication>>();
}