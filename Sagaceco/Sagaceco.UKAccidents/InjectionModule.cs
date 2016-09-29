using System;

namespace Sagaceco.UKAccidents
{
    public class InjectionModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<ViewModels.MainViewModel>().ToSelf().InSingletonScope();
        }
    }
}
