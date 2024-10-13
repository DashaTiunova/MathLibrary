using NUnit.Framework;
using LibraryRaschet; // ���������, ��� �� ���������� ���� ����������
[TestFixture]
public class TverdTeloTests
{
    private TverdTelo _tverdTelo;

    [SetUp]
    public void Setup()
    {
        _tverdTelo = new TverdTelo();
    }

    [Test]
    public void EntPitWat_ShouldReturnCorrectValue_WhenGivenTempPitWat()
    {
        // Arrange
        double tempPitWat = 30; // ������ ��������� ��������
        double expected = tempPitWat * (2 * Math.Pow(10, -7) * tempPitWat + Math.Pow(10, -4) * tempPitWat + 1.495);

        // Act
        double result = _tverdTelo.EntPitWat(tempPitWat);

        // Assert
        Assert. (expected, result, 1e-6); // ����������� �����������
    }
}


