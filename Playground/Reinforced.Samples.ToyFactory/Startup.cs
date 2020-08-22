using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reinforced.Samples.ToyFactory.Data.Context;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql;
using Reinforced.Tecture.Runtimes.EFCore.Features.Orm;

namespace Reinforced.Samples.ToyFactory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<ToyFactoryDbContext>();
            services.AddTransient(sp =>
            {
                ILazyDisposable<ToyFactoryDbContext> ld = new LazyDisposable<ToyFactoryDbContext>(() => sp.GetService<ToyFactoryDbContext>());

                TectureBuilder tb = new TectureBuilder();
                tb.WithChannel<Db>(c =>
                {
                    c.UseEfCoreOrm(ld);
                    c.UseEfCoreDirectSql(ld);
                });

                return tb.Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
