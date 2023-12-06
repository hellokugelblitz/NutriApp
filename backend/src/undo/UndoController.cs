using System;
using System.Collections.Generic;

namespace NutriApp.Undo;

public class UndoController<T> where T : UndoCommand
{
    private Stack<T> _undoStack;

    public Stack<T> UndoStack => _undoStack;

    public UndoController()
    {
        _undoStack = new Stack<T>();
    }

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
