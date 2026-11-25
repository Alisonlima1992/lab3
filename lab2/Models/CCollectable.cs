using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

public abstract class CCollectable
{
    protected Point position;
    protected Size size;
    protected double lifetime;
    protected Ellipse sprite;

    public CCollectable(Point position, double size, double lifetime)
    {
        this.position = position;
        this.size = new Size(size, size);
        this.lifetime = lifetime;

        sprite = new Ellipse
        {
            StrokeThickness = 2,
            Stroke = Brushes.Black,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Width = size,
            Height = size,
            RenderTransform = new TranslateTransform(position.X, position.Y)
        };
    }

    public Ellipse getSprite() => sprite;

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

    public abstract bool onClick(CPlayer player, CController controller, Point mousePosition);
}