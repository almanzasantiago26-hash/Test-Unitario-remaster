namespace UnitTesting;

public class MathHelper
{
    public double Add(double a, double b)
    {
        return a + b;
    }

    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("No se puede dividir entre cero.");
        return a / b;
    }
}
