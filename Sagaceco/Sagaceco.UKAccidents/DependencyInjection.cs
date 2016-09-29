using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents
{
    public static class DependencyInjection
    {
        private static IKernel kernel = null;

        public static IKernel Kernel
        {
            get
            {
                if(kernel == null)
                    kernel = new StandardKernel( new InjectionModule() );
                return kernel;
            }
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
