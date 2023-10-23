using System;

namespace NutriApp.UI;

/// <summary>
/// An abstract base class for invoking commands in the NutriApp UI.
/// </summary>
/// <typeparam name="T">The type of input this invoker handles.</typeparam>
abstract class CommandInvoker<T> : Invoker
{
    protected Command<T> command;

    /// <summary>
    /// Constructor for the CommandInvoker class.
    /// </summary>
    /// <param name="command">The command to be invoked by this invoker.</param>
    public CommandInvoker(Command<T> command)
    {
        this.command = command;
    }

    /// <summary>
    /// Abstract method to invoke the associated command. Subclasses must implement this method.
    /// </summary>
    public abstract void Invoke();
}