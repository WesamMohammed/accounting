namespace WebApplication6.Services
{
    public class Service2 : IServece2
    {
        private readonly IServece1 _servece1;

        public Service2(IServece1 servece1)
        {
            _servece1 = servece1;
        }
        public void PrinteService2(string message)
        {
            _servece1.PrinteService1(message);
        }
    }
}
