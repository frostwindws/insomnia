using System.Runtime.InteropServices;

namespace Insomnia;

/// <summary>
/// Prevent sleeping mode service.
/// </summary>
public class InsomniaService : IDisposable
{
    [Flags]
    private enum ExecutionState : uint
    {
        SystemRequired = 0x00000001,
        DisplayRequired = 0x00000002,
        AwayModeRequired = 0x00000040,
        Continuous = 0x80000000
    }

    private readonly ExecutionState _originalState;

    private InsomniaService()
    {
        _originalState = SetThreadExecutionState(ExecutionState.SystemRequired |
                                                 ExecutionState.DisplayRequired |
                                                 ExecutionState.AwayModeRequired |
                                                 ExecutionState.Continuous);
    }

    /// <inheritdoc cref="IDisposable"/>.
    public void Dispose() => SetThreadExecutionState(_originalState);

    /// <summary>
    /// Prevent sleeping mode.
    /// </summary>
    /// <returns>True if operation was successful.</returns>
    public static InsomniaService PreventSleep() => new();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern ExecutionState SetThreadExecutionState(ExecutionState flags);
}