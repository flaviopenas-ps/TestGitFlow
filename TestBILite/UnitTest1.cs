namespace TestBILite
{
    public class UnitTest1 : IClassFixture<DriverExtras>, IDisposable 
    {
        private readonly  DriverExtras driverExtras;
        public UnitTest1(DriverExtras driverExtras)
        {
            this.driverExtras = driverExtras;
        }

        public void Dispose()
        {
            //this is called after every test is done
            driverExtras.Dispose();
        }

        [Fact]
        public void Test1()
        {
            //driverExtras.setBrowserType("firefox");
            PCDiga diga = new PCDiga(driverExtras);
            Console.WriteLine("\n\nTest1 done\n\n");
        }

        [Fact]
        public void Test2()
        {
            //driverExtras.setBrowserType("firefox");
            driverExtras.NavigateTo("http://www.google.com/");
            Console.WriteLine("\n\nTest2 done\n\n");
        }
    }
}