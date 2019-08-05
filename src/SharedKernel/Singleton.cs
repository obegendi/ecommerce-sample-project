using SimpleInjector;

namespace SharedKernel
{
    public static class Singleton
    {
        public static Container Container;

        public static void Init(Container container)
        {
            Container = container;
        }
    }
}
