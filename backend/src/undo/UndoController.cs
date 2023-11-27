using System;
using System.Collections.Generic;

namespace NutriApp.Undo;

class UndoController<T> where T : UndoCommand
{
    private readonly Stack<T> _undoStack = new();

    public void Add(T command)
    {
        _undoStack.Push(command);
    }

    public void Undo()
    {
        if (_undoStack.Count > 0)
        {
            T command = _undoStack.Pop();
            command.Execute();
        }
        else
        {
            // Alert user
        }
    }
}
