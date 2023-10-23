using System;


namespace NutriApp.UI;

/// <summary>
/// An abstract base class for defining commands.
/// </summary>
/// <typeparam name="T">The type of input this command handles.</typeparam>
abstract class Command<T>

{
    protected CommandFinished onFinished;

    /// <summary>
    /// Subscribes a handler to the onFinished event.
    /// </summary>
    /// <param name="handler">The event handler to subscribe.</param>
    public void Subscribe(CommandFinished handler)
    {
        onFinished += handler;
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="userinput">The user input for the command.</param>
    public abstract void Execute(T userinput);
}

/// <summary>
/// Delegate representing the event handler for command finishing.
/// </summary>
delegate void CommandFinished();