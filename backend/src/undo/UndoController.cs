using System;
using System.Collections;
using System.Collections.Generic;

namespace NutriApp.Undo;

class UndoController<T> where T : UndoCommand<T>
{
    private readonly Stack<T> _undoStack = new();


    public void Add(T command)
    {
        _undoStack.Push(command);
    }

    public void Remove()
    {
        if (_undoStack.Count == 0)
        {
            // Alert User
        }

        _undoStack.Pop();
    }
}