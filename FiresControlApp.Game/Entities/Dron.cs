using FiresControlApp.Game.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiresControlApp.Game.Entities
{
    public class Dron
    {
        #region Public
        public int X { get; private set; }

        public int Y { get; private set; }

        public char Direction { get; private set; }

        public Forest Forest { get; private set; }
        #endregion

        protected readonly List<char> _cardinalPoints = new List<char> { 'N', 'E', 'S', 'W' };

        #region Constructors

        public Dron(int x, int y, char direction, Forest forest)
        {
            X = x;
            Y = y;
            Direction = direction;
            Forest = forest;
        }

        #endregion

        #region Public Methods

        public void ExecuteInstructions(string instructions)
        {
            char[] parsedInstructions = instructions.ToUpper().ToCharArray();

            foreach (char instruction in parsedInstructions)
            {
                switch (instruction)
                {
                    case 'M':
                        ExecuteMoveInstruction();
                        break;
                    case 'L':
                        ExecuteTurnInstruction(instruction);
                        break;
                    case 'R':
                        ExecuteTurnInstruction(instruction);
                        break;
                    default:
                        throw new GameException($"The instruction '{instruction}' is not defined");
                }
            }

            Console.WriteLine($"{X} {Y} {Direction}");
        }

        #endregion

        #region Private region

        private void ExecuteMoveInstruction()
        {
            switch (Direction)
            {
                case 'N':
                    Y += 1;
                    break;
                case 'W':
                    X -= 1;
                    break;
                case 'S':
                    Y -= 1;
                    break;
                case 'E':
                    X += 1;
                    break;
            }

            CheckPosition();
        }

        private void ExecuteTurnInstruction(char turnDirection)
        {
            int cardinalPointPosition = _cardinalPoints.IndexOf(Direction);

            if (turnDirection == 'L')
            {
                if (cardinalPointPosition - 1 < 0)
                {
                    Direction = _cardinalPoints[_cardinalPoints.Count - 1];
                }
                else
                {
                    Direction = _cardinalPoints[cardinalPointPosition - 1];
                }
            }
            else
            {
                if (cardinalPointPosition + 1 > 3)
                {
                    Direction = _cardinalPoints[0];
                }
                else
                {
                    Direction = _cardinalPoints[cardinalPointPosition + 1];
                }
            }
        }

        private void CheckPosition()
        {
            if (X < 0 || Y < 0 || X > Forest.Width || Y > Forest.High)
            {
                throw new GameException("Dron can't move out of forest");
            }
        }

        #endregion
    }
}
