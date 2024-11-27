using System.Collections.Generic;

public class CommandInvolker
{
    // A stack to keep track of executed commands.
    private Stack<ICommand> commandRegistry = new Stack<ICommand>();

    /// <summary>
    /// Process a command, which involves both executing it and registering it.
    /// </summary>
    /// <param name="commandToProcess">The command to be processed.</param>
    public void ProcessCommand(ICommand commandToProcess)
    {
        commandToProcess.Execute();
    }

    /// <summary>
    /// Register a command by adding it to the command registry stack.
    /// </summary>
    /// <param name="commandToRegister">The command to be registered.</param>
    public void RegisterCommand(ICommand commandToRegister) => commandRegistry.Push(commandToRegister);

    private bool RegistryEmpty() => commandRegistry.Count == 0;

    public void Undo()
    {
        if (!RegistryEmpty())
            commandRegistry.Pop().Undo();
    }
}
