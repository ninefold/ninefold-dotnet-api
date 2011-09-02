namespace Ninefold.Core
{
    public interface ICommandExecutor
    {
        ICommandResponse Execute(ICommand command);
    }
}