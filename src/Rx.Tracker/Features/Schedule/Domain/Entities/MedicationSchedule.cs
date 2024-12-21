using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DynamicData;
using DynamicData.Kernel;
using ReactiveMarbles.Extensions;

namespace Rx.Tracker.Features.Schedule.Domain.Entities;

/// <summary>
/// A <see cref="ISourceCache{TObject,TKey}"/> of <see cref="ScheduledMedication"/>.
/// </summary>
public class MedicationSchedule : DisposableObject, ISourceCache<ScheduledMedication, Id>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MedicationSchedule"/> class.
    /// </summary>
    /// <param name="scheduledMedications">The scheduled medications.</param>
    public MedicationSchedule(IEnumerable<ScheduledMedication> scheduledMedications)
    {
        _cache = new SourceCache<ScheduledMedication, Id>(scheduledMedication => scheduledMedication.Id).DisposeWith(Garbage);

        _cache.Edit(updater => updater.Load(scheduledMedications));
    }

    /// <summary>
    /// Gets the schedule id.
    /// </summary>
    public ScheduleId ScheduleId { get; } = new ScheduleId();

    /// <summary>
    /// Gets the user id.
    /// </summary>
    public UserId UserId { get; } = new UserId();

    /// <inheritdoc/>
    public IObservable<IChangeSet<ScheduledMedication, Id>> Connect(Func<ScheduledMedication, bool>? predicate = null, bool suppressEmptyChangeSets = true)
        => _cache.Connect(predicate, suppressEmptyChangeSets);

    /// <inheritdoc/>
    public IObservable<IChangeSet<ScheduledMedication, Id>> Preview(Func<ScheduledMedication, bool>? predicate = null) => _cache.Preview(predicate);

    /// <inheritdoc/>
    public IObservable<Change<ScheduledMedication, Id>> Watch(Id key) => _cache.Watch(key);

    /// <inheritdoc/>
    public Optional<ScheduledMedication> Lookup(Id key) => _cache.Lookup(key);

    /// <inheritdoc/>
    public void Edit(Action<ISourceUpdater<ScheduledMedication, Id>> updateAction) => _cache.Edit(updateAction);

    /// <inheritdoc/>
    public int Count => _cache.Count;

    /// <inheritdoc/>
    public IReadOnlyList<ScheduledMedication> Items => _cache.Items;

    /// <inheritdoc/>
    public IReadOnlyList<Id> Keys => _cache.Keys;

    /// <inheritdoc/>
    public IReadOnlyDictionary<Id, ScheduledMedication> KeyValues => _cache.KeyValues;

    /// <inheritdoc/>
    public Func<ScheduledMedication, Id> KeySelector => _cache.KeySelector;

    /// <inheritdoc/>
    public IObservable<int> CountChanged => _cache.CountChanged;

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly SourceCache<ScheduledMedication, Id> _cache;
}