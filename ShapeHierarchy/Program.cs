public class Shape
{
    private string? Name;

    public Shape(string name)
    {
        Name = name;
    }
    
    public string getName()
    {
         return Name??"Name";
    }
    public virtual double CalculateArea()
    {
        return 0;
    }
}


public class Circle: Shape
{
    private double Radius;
    
    public Circle(string name, double r):base(name)
    {
        Radius = r;
    }
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Rectangle: Shape
{
    private double Width;
    private double Height;
    
    public Rectangle(string name, double w, double h):base(name)
    {
        Width = w;
        Height = h;
    }

    public override double CalculateArea()
    {
        return Width * Height;
    }
}

public class Triangle: Shape
{
    private double Base;
    private double Height;

    public Triangle(string name, double b, double h):base(name)
    {
        Base = b;
        Height = h;
    }

    public override double CalculateArea()
    {
        return (Base * Height)/2;
    }
}


public class Program
{

     public static void PrintShapeArea(Shape shape)
    {
        Console.WriteLine($"Shape: {shape.getName()}, Area: {shape.CalculateArea()}");
    }

    public static void Main()
    {
        Circle circle = new Circle("circle",5);
        Rectangle rectangle = new Rectangle("rectangle",4, 6);
        Triangle triangle = new Triangle("triangle", 4, 6);

        PrintShapeArea(circle); // Output: Shape: Circle, Area: 78.53981633974483
        PrintShapeArea(rectangle);
        PrintShapeArea(triangle);
    }
}

