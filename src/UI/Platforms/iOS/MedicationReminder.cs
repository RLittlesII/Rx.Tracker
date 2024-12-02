using EventKit;
using Foundation;
using ObjCRuntime;

namespace Rx.Tracker.UI;

public class MedicationReminder : EKReminder
{
    protected internal MedicationReminder(NativeHandle handle)
        : base(handle)
    {
    }

    protected MedicationReminder(NSObjectFlag t)
        : base(t)
    {
    }
}