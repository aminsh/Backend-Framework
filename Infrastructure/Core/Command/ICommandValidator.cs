namespace Core.Command
{
    public interface ICommandValidator <in TCommand> where TCommand : ICommand
    {
        void Validate(TCommand cmd);
    }
}
