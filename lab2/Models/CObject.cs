using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

public class CObject
{
    private Point position;
    private Size size;
    private double lifetime;
    private double pointsValue;
    private Ellipse sprite;

    public CObject(Point position, double size, double lifetime)
    {
        this.position = position;
        this.size = new Size(size, size);
        this.lifetime = lifetime;

        sprite = new Ellipse
        {
            Fill = Brushes.BlueViolet,
            StrokeThickness = 2,
            Stroke = Brushes.Black,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Width = this.size.Width,
            Height = this.size.Height,
            RenderTransform = new TranslateTransform(position.X, position.Y)
        };

        pointsValue = ((1.0 / this.size.Width) / lifetime) * 1000.0;
    }

    public Ellipse getSprite() => sprite;
    public double getPointsValue() => pointsValue;

    public bool isMouseOnObject(Point mousePosition)
    {
        return mousePosition.X >= position.X && mousePosition.X <= position.X + size.Width &&
               mousePosition.Y >= position.Y && mousePosition.Y <= position.Y + size.Height;
    }

    public bool updateLifetime(double delta)
    {
        lifetime -= delta;
        return lifetime <= 0;
    }
}