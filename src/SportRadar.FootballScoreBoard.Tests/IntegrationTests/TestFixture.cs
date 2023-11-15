using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Taskmaverick.SelfSignUp.Services;

namespace SportRadar.FootballScoreBoard.Tests.IntegrationTests
{
    public class TestFixture : IAsyncLifetime
    {
        public IHost Host { get; private set; }

        public IServiceScope Scope { get; private set; }

        public async Task InitializeAsync()
        {
            Host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddFootballScoreBoardServices();
                })
                .Build();

            Scope = Host.Services.CreateScope();
        }

        public Task DisposeAsync()
        {
            Scope.Dispose();
            Host.Dispose();
            return Task.CompletedTask;
        }
    }
}
