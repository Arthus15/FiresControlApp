using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiresControlApp.Game.Abstractions;
using FiresControlApp.Game.Entities;
using FiresControlApp.Game.Enums;
using FiresControlApp.Game.Exceptions;
using Serilog;
using FormatException = FiresControlApp.Game.Exceptions.FormatException;

namespace FiresControlApp.Game.Implementations
{
    public class GameService : IGameService
    {
        #region Properties
        private Dron _dron;
        private Forest _forest;
        private IFileReaderService _fileReader;
        private List<string> _instructions;
        private List<char> _cardinalPoints = new List<char>() { 'N', 'S', 'W', 'E' };
        #endregion

        public GameService(IFileReaderService fileReader)
        {
            _fileReader = fileReader;
        }

        #region Public Methods

        public void LoadConfiguration(string path)
        {
            try
            {
                Console.WriteLine("Input:");
                _instructions = _fileReader.ReadInstructionsFile(path).ToList();
            }
            catch (Exception ex)
            {
                Log.Error($"Error loading configuration: {ex.ToString()}");
                Console.WriteLine($"Uops, something happend when Setting Game Configuration: {ex.Message}");
            }
        }

        public void Start()
        {
            try
            {
                Console.WriteLine("Output:");

                foreach (string instruction in _instructions)
                {
                    ExecuteInstruction(instruction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Uops, an error happend during game execution, please check logs for more information.");
                Log.Error($"Error happend during Game Execution: {ex.ToString()}");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Splits the current instruction, pares and gets de dimension parameters
        /// </summary>
        /// <param name="dimension">(width,high)</param>
        private void SetForestDimensions(string[] instruction)
        {
            if (_forest == null) throw new GameException("Forest can't be resize.");

            int width;
            int high;

            if (!Int32.TryParse(instruction[0], out high))
                throw new FormatException("Error parsing High Forest paremeter, please introduce a valid number");

            if (!Int32.TryParse(instruction[1], out width))
                throw new FormatException("Error parsing Width Forest paremeter, please introduce a valid number");

            _forest = new Forest() { High = high, Width = width };

        }

        private void SetDronInitialPosition(string[] instruction)
        {
            int x;
            int y;
            char direction;

            if (!Int32.TryParse(instruction[0], out x))
                throw new FormatException("Error parsing X position of Dron paremeter, please introduce a valid number.");

            if (!Int32.TryParse(instruction[1], out y))
                throw new FormatException("Error parsing Y position of Dron paremeter, please introduce a valid number.");

            if (!char.TryParse(instruction[2], out direction))
                throw new FormatException("Error parsing Direction of Dron paremeter, please introduce a valid direction (char).");

            if (x <= _forest.Width && y <= _forest.High && _cardinalPoints.Contains(direction))
            {
                _dron = new Dron(x, y, direction, _forest);
            }
            else
            {
                throw new FormatException("X position and Y position can't be hight than forest dimensions and direction must be one cardinal point");
            }

        }

        private void ExecuteInstruction(string instruction)
        {
            string[] splitedInstruction = instruction.Split(' ');

            switch (splitedInstruction.Length)
            {
                case (int)InstructionEnum.MoveDronInstruction:
                    _dron.ExecuteInstructions(instruction);
                    break;
                case (int)InstructionEnum.SetDronPositionInstruction:
                    SetDronInitialPosition(splitedInstruction);
                    break;
                case (int)InstructionEnum.SetForestDimensionInstruction:
                    SetForestDimensions(splitedInstruction);
                    break;
                default:
                    throw new GameException("Invalid Instruction");
            }
        }

        #endregion

    }
}
