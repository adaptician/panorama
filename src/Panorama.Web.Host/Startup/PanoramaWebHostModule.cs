using System;
using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using MassTransit;
using Panorama.Backing.Bus.Options;
using Panorama.Backing.Bus.Scenes;
using Panorama.Backing.Bus.Shared.Scenes;
using Panorama.Configuration;

namespace Panorama.Web.Host.Startup
{
    [DependsOn(
       typeof(PanoramaWebCoreModule))]
    public class PanoramaWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public PanoramaWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PanoramaWebHostModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            ConfigureRabbitMqTopologyUsingMassTransit();
        }

        // https://masstransit.io/documentation/configuration#configure-endpoints
        private void ConfigureRabbitMqTopologyUsingMassTransit()
        {
            // IocManager.IocContainer.Register(Component.For<ScenesConsumer>().LifestyleTransient());

            var eventBusSection = _appConfiguration.GetSection(EventBusOptions.SettingName);
            EventBusOptions eventBusOptions = new EventBusOptions();
            eventBusSection.Bind(eventBusOptions);
            
            var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.Host(eventBusOptions.RabbitMq.HostName, host =>
                {
                    host.Username(eventBusOptions.RabbitMq.UserName);
                    host.Password(eventBusOptions.RabbitMq.Password);
                });
                
                // config.Publish<ScenesRequestedEto>(x =>
                // {
                //     x.Durable = false; // default: true
                //     x.AutoDelete = true; // default: false
                //     x.ExchangeType = "direct"; // default, allows any valid exchange type
                // });

                config.ReceiveEndpoint(queueName: "Scenes", endpoint =>
                {
                    // endpoint.Bind("exchange-name", x =>
                    // {
                    //     x.Durable = false;
                    //     x.AutoDelete = true;
                    //     x.ExchangeType = "direct";
                    //     x.RoutingKey = "dummythis";
                    // });
                    //
                    endpoint.Bind<ScenesRequestedEto>();
                    
                    // endpoint.Handler<ScenesRequestedEto>(async context =>
                    // {
                    //     using (var consumer = IocManager.ResolveAsDisposable<ScenesConsumer>(typeof(ScenesConsumer)))
                    //     {
                    //         await consumer.Object.Consume(context);
                    //     }
                    // });
                });
            });

            IocManager.IocContainer.Register(Component.For<IBus, IBusControl>().Instance(busControl));

            busControl.Start();
        }
    }
}
