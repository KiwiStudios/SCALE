namespace SCALE.Events;

/// <summary>
/// Will generate an Emit<SIGNALNAME>(<PARAMETERS>) method on EventBus.
/// </summary>
[AttributeUsage(AttributeTargets.Delegate)]
public sealed class GenerateMethodForEventHandlerAttribute : Attribute
{
}