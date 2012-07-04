﻿using System.Linq;
using Autofac;
using EPiServer;
using EPiServer.Core;
using EPiServer.Core.PropertySettings;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using PageTypeBuilder.Abstractions;
using PageTypeBuilder.Configuration;
using PageTypeBuilder.Discovery;
using PageTypeBuilder.Reflection;
using PageTypeBuilder.Synchronization;
using PageTypeBuilder.Synchronization.Validation;
using InitializationModule=EPiServer.Web.InitializationModule;

namespace PageTypeBuilder
{
    [ModuleDependency(typeof(InitializationModule))]
    public class Initializer : IInitializableModule 
    {
        public void Initialize(InitializationEngine context)
        {
            var containerBuilder = new ContainerBuilder();
            var defaultBootstrapper = new DefaultBootstrapper();
            defaultBootstrapper.Configure(containerBuilder);
            var container = containerBuilder.Build();
            
            PageTypeSynchronizer synchronizer = container.Resolve<PageTypeSynchronizer>();
            synchronizer.SynchronizePageTypes();

            DataFactory.Instance.LoadedPage += DataFactory_LoadedPage;
            DataFactory.Instance.LoadedChildren += DataFactory_LoadedChildren;
            DataFactory.Instance.LoadedDefaultPageData += DataFactory_LoadedPage;
        }

        public void Preload(string[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public void Uninitialize(InitializationEngine context)
        {
            DataFactory.Instance.LoadedPage -= DataFactory_LoadedPage;
            DataFactory.Instance.LoadedChildren -= DataFactory_LoadedChildren;
            DataFactory.Instance.LoadedDefaultPageData -= DataFactory_LoadedPage;
        }

        static void DataFactory_LoadedPage(object sender, PageEventArgs e)
        {
            if(e.Page == null)
                return;

            e.Page = PageTypeResolver.Instance.ConvertToTyped(e.Page);
        }

        static void DataFactory_LoadedChildren(object sender, ChildrenEventArgs e)
        {
            for (int i = 0; i < e.ChildrenItems.Count; i++)
            {
                var page = e.ChildrenItems[i] as PageData;
                if (page != null)
                {
                    e.ChildrenItems[i] = PageTypeResolver.Instance.ConvertToTyped(page);
                }
            }
        }

        private PageTypeBuilderConfiguration Configuration
        {
            get
            {
                return PageTypeBuilderConfiguration.GetConfiguration();
            }
        }
    }
}