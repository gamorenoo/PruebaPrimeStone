using Common.Automapper;
using Common.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PruebaPrimeStone.Estudiantes.Api.AplicationServices;
using PruebaPrimeStone.Estudiantes.Api.BackGroundServices;
using PruebaPrimeStone.Estudiantes.Repository.Repository;
using PruebaPrimeStone.Estudiantes.Repository.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaPrimeStone.Estudiantes.Api.DI
{
    /// <summary>
    /// Inyeccion de dependencias
    /// </summary>
    public class DependencyInjectionProfile
    {
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public DependencyInjectionProfile(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void InjectDependencies(IServiceCollection services)
        {
            services.AddDbContext<AplicacionDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(typeof(GlobalMapperProfile));

            #region Repositorios
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<EstudianteRepository>();
            services.AddTransient<CursoRepository>();
            services.AddTransient<DireccionRepository>();
            #endregion

            #region Servicios de aplicacion
            services.AddTransient<EstudianteAppService>();
            services.AddTransient<CursoAppService>();

            #endregion

            #region BackgroundServices

            services.AddHostedService<TestHostedService>();
            // services.AddTransient<IHostedService, TestHostedService >();

            #endregion
        }
    }
}
