using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace NutriApp.Undo;

class UndoController<T> where T : UndoCommand
{
    private Stack<T> _undoStack { get; }

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
            throw new Exception("There is no commands to undo.");
        }
    }
}
