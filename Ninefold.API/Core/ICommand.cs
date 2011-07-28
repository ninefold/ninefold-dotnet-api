namespace Ninefold.API.Core
{
    public interface ICommand
    {
        void Prepare();
        ICommandResponse Execute();        
    }
}
