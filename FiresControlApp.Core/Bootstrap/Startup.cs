using FiresControlApp.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FiresControlApp.Core.Bootstrap
{
    /// <summary>
    /// Contains the main bootstrap operations such as dependency injection registrations,
    /// appsettings configurations and logging.
    /// </summary>
    public abstract class Startup
    {
        protected readonly IServiceCollection _services = new ServiceCollection();

        protected Startup()
        {
            RegisterCore();
            RegisterPartials();
        }

        #region Public Methods

        /// <summary>
        /// Builds the service collection into a service provider instance.
        /// </summary>
        /// <returns>Service provider with all the pre-registered dependencies.</returns>
        public ServiceProvider Build()
        {
            return _services.BuildServiceProvider();
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Registers the configuration of the Daemon.
        /// </summary>
        /// <remarks>
        /// Must be overriden by the WindowsService layer implementation.
        /// </remarks>
        protected abstract void RegisterPartials();

        #endregion

        #region Private Methods

        /// <summary>
        /// Registers core dependencies and configurations.
        /// </summary>
        private void RegisterCore()
        {
            string application = AppDomain.CurrentDomain.FriendlyName;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"C:\\FireControlApp\\Logs\\{application}\\{application}.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: int.MaxValue)
                .CreateLogger();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Registers a partial Startup with its configuration and its services.
        /// </summary>
        /// <typeparam name="T">Partial type to register.</typeparam>
        protected void RegisterPartial<T>() where T : IPartialStartup, new()
        {
            T partialStartup = new T();

            partialStartup.RegisterServices(_services);
        }


        #endregion
    }
}
