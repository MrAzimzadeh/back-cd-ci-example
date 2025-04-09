namespace testing;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    
    [Test]
    public void TestCurrentSecondOddOrEven()
    {
        int currentSecond = DateTime.Now.Second;

        if (currentSecond % 2 == 0)
        {
            Assert.Fail("The current second is even, test failed.");
        }
        else
        {
            Assert.Pass("The current second is odd, test passed.");
        }
    }
}