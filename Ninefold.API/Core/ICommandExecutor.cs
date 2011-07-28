namespace Ninefold.API.Core
{
    public interface ICommandExecutor
    {
        ICommandResponse Execute(ICommand command);
    }
}