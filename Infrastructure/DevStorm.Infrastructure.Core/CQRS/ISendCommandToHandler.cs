namespace DevStorm.Infrastructure.Core.CQRS
{
    public interface ISendCommandToHandler
    {
        object Handle(CommnadMessage message);
    }
}
