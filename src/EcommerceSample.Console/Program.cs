using System;
using System.IO;
using ApplicationServices.Campaigns;
using ApplicationServices.Demands;
using ApplicationServices.Orders;
using ApplicationServices.Products;
using ApplicationServices.Time;
using CampaignManagement.Core;
using CommandHandlers;
using CommandHandlers.Contracts;
using EcommerceSample.Data;
using EcommerceSample.Data.Contracts;
using EcommerceSample.TimeSimulator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Core;
using ProductManagement.Core;
using SharedKernel;
using SharedKernel.Contracts;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace EcommerceSample.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            // Register stuff
            RegisterContext(container);
            RegisterServices(container);
            container.Verify();

            ReadFromFile(container);

            Console.ReadLine();
        }

        public static void ReadFromFile(Container container)
        {
            var lines = File.ReadAllLines($@"{Directory.GetCurrentDirectory()}/data.txt");
            var resolver = container.GetInstance<ICommandParserStrategyResolver>();
            foreach (var line in lines)
            {
                var commands = line.Trim().Split(" ");
                var method = resolver.Resolver(commands);
                method.Execute(commands);
            }
        }

        private static void RegisterServices(Container container)
        {
            container.Register<SystemTimer>(Lifestyle.Singleton);
            container.Register<SubscriptionManager>(Lifestyle.Singleton);
            container.Register<IConsoleWriter, ConsoleWriter>(Lifestyle.Transient);
            container.Register<ICommandParserStrategyResolver, CommandParserStrategyResolver>();
            container.Collection.Register(typeof(ICommandStrategy), typeof(ICommandStrategy).Assembly);
            container.Collection.Register(typeof(IHandler<>), typeof(ProductServices).Assembly);
            container.Register<IRepository<Product>, Repository<Product>>();
            container.Register<IRepository<Order>, Repository<Order>>();
            container.Register<IRepository<Campaign>, Repository<Campaign>>();
            container.Register<IRepository<Sale>, Repository<Sale>>();
            container.Register<IUnitOfWork, UnitOfWork>();
            container.Register<IDomainEventsDispatcher, DomainEventsDispatcher>();
            container.Register<IDemandServices, DemandServices>();
            container.Register<IProductServices, ProductServices>();
            container.Register<IOrderServices, OrderServices>();
            container.Register<ICampaignServices, CampaignServices>();
            container.Register<ITimeServices, TimeServices>();


            Singleton.Init(container);
        }

        private static void RegisterContext(Container container)
        {
            // Build an IServiceProvider with DbContext pooling and resolve a scope factory.
            var scopeFactory = new ServiceCollection()
                .AddDbContextPool<EcommerceContext>(o => o.UseInMemoryDatabase(nameof(EcommerceContext)))
                .BuildServiceProvider(true)
                .GetRequiredService<IServiceScopeFactory>();

            // Use that scope factory to register an IServiceScope into Simple Injector
            container.Register(scopeFactory.CreateScope, Lifestyle.Singleton);

            // Cross wire the DbContext by resolving the IServiceScope and requesting the
            // DbContext from that scope.
            container.Register(() => container.GetInstance<IServiceScope>().ServiceProvider
                    .GetService<EcommerceContext>(),
                Lifestyle.Singleton);
        }
    }
}
