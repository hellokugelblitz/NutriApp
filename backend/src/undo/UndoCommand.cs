using System;

namespace NutriApp.Undo;

public abstract class UndoCommand
{
    protected UndoCommandFinished onFinished;

    public void Subscribe(UndoCommandFinished handler)
    {
        onFinished += handler;
    }

    public abstract void Execute();
}

public delegate void UndoCommandFinished();
