using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;
using WMDesktopUI.ViewModels;
using WMDesktopUI.Views;

namespace WMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WHProductModel, WareHouseProductModel>();
                cfg.CreateMap<WareHouseProductModel, WHProductModel>();
                cfg.CreateMap<WareHouseProductModel, WHPostProductModel>();
            });

            var mapper = config.CreateMapper();

            _container.Instance(mapper);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            GetType().Assembly
                .GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(viewModelType,
                viewModelType.ToString(), viewModelType));

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
