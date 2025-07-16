using Microsoft.Maui.Controls;
using Rx.Tracker.Features.Medications.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Rx.Tracker.UI.Features.Medicine.Add;

public class DosageTypeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value as IEnumerable<Dosage>;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}