using VContainer;
using VContainer.Unity;


namespace _Project.Scripts
{
    public class GlobalScope : LifetimeScope 
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CollectableObjectInventory>(Lifetime.Singleton);
        }
    }
}