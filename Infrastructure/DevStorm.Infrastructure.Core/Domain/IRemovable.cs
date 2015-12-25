
namespace DevStorm.Infrastructure.Core.Domain
{
    public interface IRemovable
    {
        bool IsDeleted { get; set; }
    }

    public static class RemovableExtension
    {
        public static void Remove(this IRemovable removable)
        {
            removable.IsDeleted = true;
        } 
    }
}
