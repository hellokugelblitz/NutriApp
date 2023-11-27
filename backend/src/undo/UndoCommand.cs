using System;

namespace NutriApp.Undo;

public abstract class UndoCommand
{
    protected CommandFinished onFinished;

    public void Subscribe(CommandFinished handler)
    {
        onFinished += handler;
    }

    public abstract void Execute();
}

public delegate void CommandFinished();
