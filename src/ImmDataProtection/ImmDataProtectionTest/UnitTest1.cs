namespace ImmDataProtectionTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //var sample = new Sample<Items>();
            //var a = sample.GetName();
            //Assert.Equal(a,"A");

            Enum.TryParse("A", out Items myItem);
            Assert.Equal(myItem,Items.None);
        }
    }
}