using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using static Lab1.Program;

namespace Lab1Tests;

public class Lab1UnitTests
{
    [Theory]
    [InlineData(3)]
    [InlineData(8)]
    [InlineData(16)]
    [InlineData(32)]
    [InlineData(109)]
    public void DecimalToBinaryTest(int number)
    {
        string expectedBinary = Convert.ToString(number, 2);
        string result = DecimalToBinary(number);
        Assert.Equal(expectedBinary, result);
    }

    [Theory]
    [InlineData("110", 1, true)]
    [InlineData("110", 2, false)]
    [InlineData("1010", 2, true)]
    [InlineData("1000000", 6, true)]
    [InlineData("101010101010", 6, true)]
    public void ZerosCountTest(string binary, int k, bool expected)
    {
        bool result = ZerosCount(binary, k);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(8, 1, 3)] // 10, 101, 110
    [InlineData(8, 0, 3)] // 1, 11, 111
    [InlineData(16, 3, 1)] // 1000
    [InlineData(32, 4, 1)] // 10000
    [InlineData(100, 5, 7)] // 100000, 1000001, 1000010, 1000100, 1001000, 1010000, 1100000
    public void BinaryNumbersCountTest(int n, int k, int expectedCount)
    {
        int result = BinaryNumbersCount(n, k);
        Assert.Equal(expectedCount, result);
    }

    [Theory]
    [InlineData("100 2", true, new[] { 100, 2 })]
    [InlineData("sf ad", false, null)]
    [InlineData("12 12 12", false, null)]
    [InlineData("1", false, null)]
    [InlineData("70, 70", false, null)]
    public void ParseInputTest(string input, bool parseRes, int[]? expectedResult)
    {
        File.WriteAllText("tempINPUT.txt", input);
        bool parse = true;
        int[]? result = null;
        try
        {
            result = ParseInput("tempINPUT.txt");
        }
        catch (Exception)
        {
            parse = false;
        }
        Assert.Equal(parseRes, parse);
        Assert.Equal(result, expectedResult);
        File.Delete("tempINPUT.txt");
    }

    [Theory]
    [InlineData(-5, false)]
    [InlineData(10, true)]
    [InlineData(109, true)]
    [InlineData(1, true)]
    [InlineData(110, false)]
    public void IsInRangeTest(int value, bool expectedResult)
    {
        Assert.Equal(expectedResult, IsInRange(value));
    }
}