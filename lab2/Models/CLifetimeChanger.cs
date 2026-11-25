using System.Windows;
using System.Windows.Media;

public class CLifetimeChanger : CCollectable
{
    public CLifetimeChanger(Point position, double size, double lifetime)
        : base(position, size, lifetime)
    {
        sprite.Fill = Brushes.Yellow;
    }

    public override string onClick(CPlayer player, CController controller, Point mousePosition)
    {
        if (!isMouseOnObject(mousePosition)) return null;

        controller.changeLifetimeRange(0.5, 2.5);
        return "Время жизни изменено!";
    }
}