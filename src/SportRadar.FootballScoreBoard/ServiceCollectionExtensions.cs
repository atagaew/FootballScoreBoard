using Microsoft.Extensions.DependencyInjection;
using SportRadar.FootballScoreBoard;

namespace Taskmaverick.SelfSignUp.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFootballScoreBoardServices(this IServiceCollection services)
        {
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IMatchReportService, MatchReportService>();
            services.AddScoped<IMatchRepository, MatchInMemoryRepository>();
            return services;
        }
    }
}