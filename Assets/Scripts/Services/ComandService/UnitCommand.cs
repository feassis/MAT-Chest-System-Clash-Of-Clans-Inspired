
/// <summary>
/// An abstract class representing a unit-related command.
/// </summary>
public abstract class UnitCommand : ICommand
{
    
    /// <summary>
    /// Abstract method to execute the unit command. Must be implemented by concrete subclasses.
    /// </summary>
    public abstract void Execute();

    public abstract void Undo();

    /// <summary>
    /// Abstract method to determine whether the command will successfully hit its target.
    /// Must be implemented by concrete subclasses.
    /// </summary>
    public abstract bool WillSucced();

}
