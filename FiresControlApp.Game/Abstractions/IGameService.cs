using System;
using System.Collections.Generic;
using System.Text;

namespace FiresControlApp.Game.Abstractions
{
    public interface IGameService
    {
        void LoadConfiguration(string path);
        void Start();
    }
}
