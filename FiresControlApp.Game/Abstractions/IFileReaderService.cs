using System;
using System.Collections.Generic;
using System.Text;

namespace FiresControlApp.Game.Abstractions
{
    public interface IFileReaderService
    {
        IEnumerable<string> ReadInstructionsFile(string path);
    }
}
