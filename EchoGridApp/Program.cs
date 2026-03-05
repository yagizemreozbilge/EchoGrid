internal class Program {
  private static void Main(string[] args) {
    Console.WriteLine("EchoGrid Application Running..");
    var echoGridLibrary = new EchoGridLibrary.EchoGrid();
    echoGridLibrary.Add(2, 2);
    echoGridLibrary.Multiply(2, 2);
    echoGridLibrary.Subtract(2, 2);
    echoGridLibrary.Divide(2, 2);
  }
}
