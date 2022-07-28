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
            driverExtras.Dispose();
        }

        [Fact]
        public void Test1()
        {
            PCDiga diga = new PCDiga(driverExtras);
            //passo 2
            //passo 3

        }

        [Fact]
        public void Test2()
        {
            driverExtras.NavigateTo("http://192.168.1.66:4444/");
        }
    }
}