using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Context;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PruebaPrimeStone.Estudiantes.Repository.Repository;

namespace PruebaPrimeStone.Estudiantes.Api.BackGroundServices
{
    class TestHostedService : IHostedService, IDisposable
    {
        #region Properties
        private int executionCount = 0;
        private Timer _timer = null!;
        public IServiceProvider Services { get; }
        #endregion

        #region Builder
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="doWorkRepository"></param>
        public TestHostedService(IServiceProvider services)
        {
            Services = services;
        }
        #endregion

        #region Public Methods

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            executionCount++;
            using (var scope = Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AplicacionDBContext>();
                DoWork doWork = new DoWork()
                {
                    Id = Guid.NewGuid(),
                    EstadoBorrado = false,
                    Evento = "Meanje enviado Número: " + executionCount,
                    Fecha = DateTime.Now
                };
                context.DoWork.Add(doWork);
                context.SaveChanges();
            }

        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
        #endregion
    }
}
