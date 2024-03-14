using MaToSD_2;

namespace MaToSD_2UnitTests;

public class UnitTest1
{
    [Fact]
    public async Task HtmlConverter_ReturnsStringAndEqualsToExpectedResult()
    {
        //Arrange
        string filePath = "../../../TestFiles/CheckForResult.md";
        string expectedResultPath = "../../../TestFiles/ExpectedHtmlResult.html";

        //Act
        string text = await File.ReadAllTextAsync(filePath);
        string expectedResult = await File.ReadAllTextAsync(expectedResultPath);
        string result = HTMLConverter.Convert(text);

        //Assert
        Assert.True(!String.IsNullOrEmpty(result));
        Assert.Equal(result, expectedResult);
    }

    [Fact]
    public async Task ANSIConverter_ReturnsStringAndEqualsToExpectedResult()
    {
        //Arrange
        string filePath = "../../../TestFiles/CheckForResult.md";
        string expectedResultPath = "../../../TestFiles/ExpectedAnsiResult.txt";

        //Act
        string text = await File.ReadAllTextAsync(filePath);
        string expectedResult = await File.ReadAllTextAsync(expectedResultPath);
        string result = ANSIConverter.Convert(text);

        //Assert
        Assert.True(!String.IsNullOrEmpty(result));
        Assert.Equal(result, expectedResult);
    }
}