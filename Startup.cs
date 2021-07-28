using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job.Advertisement.Service.Entities;
using Job.Advertisement.Service.Repositories;
using Job.Advertisement.Service.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Nest;

namespace Job.Advertisement.Service
{
    public class Startup
    {
        private ServiceSettings serviceSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddSingleton(provider =>
            {

                var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionName);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            services.AddSingleton<IAdvertRepositories, AdvertRepositories>();
            services.AddSingleton<IEmployerRepositories, EmployerRepositories>();

            AddElasticsearch(services);

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));
            services.AddControllers(options =>
            {

                options.SuppressAsyncSuffixInActionNames = false;
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Job.Advertisement.Service", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Job.Advertisement.Service v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void AddElasticsearch(IServiceCollection services)
        {
            var elasticSettings = Configuration.GetSection(nameof(ElasticSearchSettings)).Get<ElasticSearchSettings>();
            var url = elasticSettings.ElasticSearchUrl;
            var defaultIndex = elasticSettings.IndexName;

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<Advert>(m => m
                    .Ignore(p => p.Salary)
                    .Ignore(p => p.SideBenefits)
                    .Ignore(p => p.WorkType)
                    .Ignore(p => p.FirmId)
                    .Ignore(p => p.AdvertDescription)
                    .Ignore(p => p.AdvertQuality)
                    .Ignore(p => p.Position)
                    .PropertyName(p => p.Id, "id")
                    .PropertyName(p => p.Airtime, "airtime")

                );


            var client = new ElasticClient(settings);


            services.AddSingleton<IElasticClient>(client);
        }
    }
}
