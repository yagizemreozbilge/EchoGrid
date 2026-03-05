
namespace EchoGridLibrary.Tests {
public class EchoGridTests {
  [Fact]
  public void TestAdd() {
    EchoGrid calc = new EchoGrid();
    Assert.Equal(4, calc.Add(2, 2));
  }

  [Fact]
  public void TestSubtract() {
    EchoGrid calc = new EchoGrid();
    Assert.Equal(2, calc.Subtract(4, 2));
  }

  [Fact]
  public void TestMultiply() {
    EchoGrid calc = new EchoGrid();
    Assert.Equal(8, calc.Multiply(2, 4));
  }

  [Fact]
  public void TestDivide() {
    EchoGrid calc = new EchoGrid();
    Assert.Equal(2, calc.Divide(4, 2));
  }

  [Fact]
  public void TestDivideByZero() {
    EchoGrid calc = new EchoGrid();
    Assert.Throws<DivideByZeroException>(() => calc.Divide(4, 0));
  }
}
}
