using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.Maui.Controls;
using static Rx.Tracker.Features.Medications.ViewModels.AddMedicineStateMachine;

namespace Rx.Tracker.UI.Features.Medicine.Add;

public class AddMedicineStateConverter : IValueConverter
{
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1025:Code should not contain multiple whitespace in a row", Justification = "switch expression.")]
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var state = value is AddMedicineState;
        if (!state)
        {
            return null;
        }

        return value switch
        {
            AddMedicineState.Initial => true,
            AddMedicineState.Busy => true,
            AddMedicineState.Loaded => true,
            AddMedicineState.Failed  => false,
            var _                    => null
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}