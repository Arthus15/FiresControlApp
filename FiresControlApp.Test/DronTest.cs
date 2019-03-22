using FiresControlApp.Game.Entities;
using FiresControlApp.Game.Exceptions;
using System;
using Xunit;

namespace FiresControlApp.Test
{
    public class DronTest
    {
        private readonly Forest _forest;

        public DronTest()
        {
            _forest = new Forest() { Width = 5, High = 5 };
        }

        [Theory]
        [InlineData("M")]
        [InlineData("LLMMMMMMMM")]
        public void Moving_Out_Of_Range_Throws_Exception(string instruction)
        {
            //Arrange
            var dron = new Dron(0, 0, 'S', _forest);

            //Act & Assert
            Assert.Throws<GameException>(() => dron.ExecuteInstructions(instruction));
        }

        [Fact]
        public void Moving_South_As_Expected()
        {
            //Arrange
            var dron = new Dron(0, 1, 'S', _forest);

            //Act
            dron.ExecuteInstructions("M");

            //Assert
            Assert.Equal<int>(0, dron.Y);

        }

        [Fact]
        public void Moving_West_As_Expected()
        {
            //Arrange
            var dron = new Dron(1, 1, 'W', _forest);

            //Act
            dron.ExecuteInstructions("M");

            //Assert
            Assert.Equal<int>(0, dron.X);

        }

        [Fact]
        public void Moving_North_As_Expected()
        {
            //Arrange
            var dron = new Dron(1, 1, 'N', _forest);

            //Act
            dron.ExecuteInstructions("M");

            //Assert
            Assert.Equal<int>(2, dron.Y);

        }

        [Fact]
        public void Moving_East_As_Expected()
        {
            //Arrange
            var dron = new Dron(1, 1, 'E', _forest);

            //Act
            dron.ExecuteInstructions("M");

            //Assert
            Assert.Equal<int>(2, dron.X);

        }
    }
}
