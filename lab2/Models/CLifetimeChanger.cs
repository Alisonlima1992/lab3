using System.Windows;
using System.Windows.Media;

public class CLifetimeChanger : CCollectable
{
    public CLifetimeChanger(Point position, double size, double lifetime)
        : base(position, size, lifetime)
    {
        sprite.Fill = Brushes.Yellow;
    }

    public override bool onClick(CPlayer player, CController controller, Point mousePosition)
    {
        if (!isMouseOnObject(mousePosition)) return false;
        controller.changeLifetimeRange(0.5, 3.0);
        return true;
    }
}