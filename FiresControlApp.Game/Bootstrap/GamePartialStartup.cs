using FiresControlApp.Core.Abstractions;
using FiresControlApp.Game.Abstractions;
using FiresControlApp.Game.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiresControlApp.Game.Bootstrap
{
    public class GamePartialStartup : IPartialStartup
    {

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IFileReaderService, FileReaderService>();
            services.AddSingleton<IGameService, GameService>();
        }
    }
}
