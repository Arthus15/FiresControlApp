using FiresControlApp.Core.Bootstrap;
using FiresControlApp.Game.Abstractions;
using FiresControlApp.Game.Bootstrap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FiresControlApp.WindowsService.Bootstrap
{
    public class FiresControlStartup : Startup
    {
        private static void Main(string[] args)
        {
            IServiceProvider serviceProvider = new FiresControlStartup().Build();

            IGameService gameService = (IGameService)serviceProvider.GetService(typeof(IGameService));

            Console.WriteLine("Please introduce the Instruction File path...");

            string path = Console.ReadLine();

            if (!string.IsNullOrEmpty(path))
            {
                gameService.LoadConfiguration(path);
                gameService.Start();
            }
            else
            {
                Console.WriteLine("Path can't be empty...Finishing execution...");
            }

            Console.WriteLine("Game Finished...");
            Task.Delay(-1).Wait();
        }


        protected override void RegisterPartials()
        {
            RegisterPartial<GamePartialStartup>();
        }
    }
}
