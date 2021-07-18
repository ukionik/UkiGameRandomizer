using Ninject;

namespace UkiRetroGameRandomizer.Configuration
{
    public class DIFactory
    {
        public static IKernel CreateNinject()
        {
            return new StandardKernel(new DIConfiguration(typeof(DIFactory).Assembly));
        }
    }
}