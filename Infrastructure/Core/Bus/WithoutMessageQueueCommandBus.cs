using Core.Api;
using Core.ApiResult;
using Core.Command;
using Core.Domain.Contract;

namespace Core.Bus
{
    public class WithoutMessageQueueCommandBus : ICommandBus
    {
        public void Send<TCommand>(TCommand cmd) where TCommand : ICommand
        {
            var message = new CommnadMessage(
                cmd, 
                cmd.GetType().Name, 
                DependencyManager.Resolve<ICurrent>());

            var result = DependencyManager.Resolve<IResult>();
            result.Command = new {id = cmd.CommandId};

            var validationResult = DependencyManager.Resolve<ISendCommandToValidator>().Validate(message);

            if (validationResult.IsNotValid)
            {
                result.ValidationResult = validationResult.ToDto();
                return;
            }

            var returnValue = DependencyManager.Resolve<ISendCommandToHandler>().Handle(message);

            result.ValidationResult = new {isValid = true};
            result.ReturnValue = returnValue;
        }
    }
}
