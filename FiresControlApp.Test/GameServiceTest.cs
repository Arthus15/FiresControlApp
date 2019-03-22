using FiresControlApp.Game.Abstractions;
using FiresControlApp.Game.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using FormatException = FiresControlApp.Game.Exceptions.FormatException;

namespace FiresControlApp.Test
{
    public class GameServiceTest
    {
        private GameService _gameService;

        public GameServiceTest()
        {
        }

        [Fact]
        public void Game_Executes_AsExpected()
        {
            try
            {
                // Arrange
                var mockFileReaderService = new Mock<IFileReaderService>();

                mockFileReaderService.Setup(x => x.ReadInstructionsFile(It.IsAny<string>())).Returns(new List<string>() { "5 5", "3 3 E", "MMRMMRMRRM" });

                _gameService = new GameService(mockFileReaderService.Object);

                _gameService.LoadConfiguration("");

                // Act
                _gameService.Start();
            }
            catch (Exception ex)
            {
                // Assert
                Assert.False(true);
            }
        }

        [Fact]
        public void Wrong_Dron_Initial_Position_Controls_Exception()
        {
            try
            {
                // Arrange
                var mockFileReaderService = new Mock<IFileReaderService>();

                mockFileReaderService.Setup(x => x.ReadInstructionsFile(It.IsAny<string>())).Returns(new List<string>() { "5 5", "6 6 E", "MMMM" });

                _gameService = new GameService(mockFileReaderService.Object);

                _gameService.LoadConfiguration("");

                // Act
                _gameService.Start();
            }
            catch (Exception ex)
            {
                // Assert
                Assert.False(true);
            }

        }
    }
}
