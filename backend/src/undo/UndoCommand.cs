using System;
using NutriApp.UI;

namespace NutriApp.Undo;

abstract class UndoCommand<T>
{
    protected CommandFinished onFinished;

    public void Subscribe(CommandFinished handler)
    {
        onFinished += handler;
    }

    public abstract void Execute();
}

delegate void CommandFinished();