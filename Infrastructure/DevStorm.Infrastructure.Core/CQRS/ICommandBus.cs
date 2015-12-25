namespace DevStorm.Infrastructure.Core.CQRS
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand cmd) where TCommand : ICommand;
    }

}
