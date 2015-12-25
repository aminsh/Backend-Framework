namespace DevStorm.Infrastructure.Core.CQRS
{
    public interface ICommandValidator <in TCommand> where TCommand : ICommand
    {
        void Validate(TCommand cmd);
    }
}
