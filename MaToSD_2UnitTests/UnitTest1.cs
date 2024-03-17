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

    [Fact]
    public async Task HtmlConverterCreatesFile_FileShouldExistAndBeEqualToExpected()
    {
        //Arrange
        string filePath = "../../../TestFiles/CheckForResult.md";
        string expectedResultPath = "../../../TestFiles/ExpectedHtmlResult.html";

        //Act
        string text = await File.ReadAllTextAsync(filePath);
        string expectedResult = await File.ReadAllTextAsync(expectedResultPath);
        string result = HTMLConverter.Convert(text);
        await HTMLConverter.CreateHtmlFile(result, filePath);
        string resultFromFile = await File.ReadAllTextAsync(Path.ChangeExtension(filePath, ".html"));

        //Assert
        Assert.True(File.Exists(Path.ChangeExtension(filePath, ".html")));
        Assert.Equal(expectedResult, resultFromFile);

        File.Delete(Path.ChangeExtension(filePath, ".html"));
    }
}