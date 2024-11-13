using System;
using System.IO;
using Xunit;

namespace mkr.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_ValidInput()
        {
            string[] lines = { "1345 2584", "2584 683", "2584 1345", "683 1345", "683 1345", "2584 683" };

            var ex = Record.Exception(() => Program.ValidateInput(lines));
            Assert.Null(ex);
        }

        [Fact]
        public void Test_InputLessThanRequired()
        {
            string[] lines = { "1345 2584", "2584 683" };

            var ex = Record.Exception(() => Program.ValidateInput(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("Кількість рядків вхідної інформації має бути рівно 6", ex.Message);
        }

        [Fact]
        public void Test_InvalidNumberFormat()
        {
            string[] lines = { "abc def", "2584 683", "2584 1345", "683 1345", "683 1345", "2584 683" };

            var ex = Record.Exception(() => Program.ValidateInput(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("Обидва значення в кожному рядку мають бути додатними цілими числами", ex.Message);
        }

        [Fact]
        public void Test_ValidProcessing_PossibleCase()
        {
            string[] lines = { "1345 2584", "2584 683", "2584 1345", "683 1345", "683 1345", "2584 683" };
            string expected = "POSSIBLE";

            string result = Program.ProcessLines(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_ValidProcessing_ImpossibleCase()
        {
            string[] lines = { "1 2584", "2584 683", "2584 1345", "683 1345", "683 1345", "2584 683" };
            string expected = "IMPOSSIBLE";

            string result = Program.ProcessLines(lines);

            Assert.Equal(expected, result);
        }
    }
}
