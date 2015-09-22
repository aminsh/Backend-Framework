namespace Core.Command
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand cmd) where TCommand : ICommand;
    }

}
