using FiresControlApp.Game.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FiresControlApp.Game.Implementations
{
    public class FileReaderService : IFileReaderService
    {
        public FileReaderService()
        {

        }

        public IEnumerable<string> ReadInstructionsFile(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);

            using(StreamReader reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Console.WriteLine(line);
                    yield return line;
                }
            }
        }
    }
}
