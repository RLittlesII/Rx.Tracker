using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Controls;
using Rx.Tracker.Features.Medications.Domain.Entities;

namespace Rx.Tracker.UI.Features.Medicine.Add;

public class DosageTypeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is not IEnumerable<Dosage> dosages ? null : dosages;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}