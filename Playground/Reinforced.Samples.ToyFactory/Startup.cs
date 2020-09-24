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
using Reinforced.Samples.ToyFactory.Data;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm;

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
                var ld = new LazyDisposable<ToyFactoryDbContext>(() => sp.GetService<ToyFactoryDbContext>());

                var tb = new TectureBuilder();
                
                tb.WithChannel<Db>(c =>
                {
                    c.UseEfCoreOrm(ld);
                    c.UseEfCoreDirectSqlCommand(ld);
                    //c.UseEfCoreOrmCommand(ld);
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
           
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
