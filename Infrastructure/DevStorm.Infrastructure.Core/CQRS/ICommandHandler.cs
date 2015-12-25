namespace DevStorm.Infrastructure.Core.CQRS
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand cmd);
    }
}
