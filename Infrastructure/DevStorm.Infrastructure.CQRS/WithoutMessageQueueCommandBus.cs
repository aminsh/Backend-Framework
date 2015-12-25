using DevStorm.Infrastructure.Core.Api;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.Domain;
using DevStorm.Infrastructure.Core.IOC;

namespace DevStorm.Infrastructure.CQRS
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
                result.ValidationResult = validationResult;
                return;
            }

            var returnValue = DependencyManager.Resolve<ISendCommandToHandler>().Handle(message);

            result.ValidationResult = validationResult;
            result.ReturnValue = returnValue;
        }
    }
}