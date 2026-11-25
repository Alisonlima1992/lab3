using System.Windows;
using System.Windows.Media;

public class CPointGiver : CCollectable
{
    private double pointsValue;

    public CPointGiver(Point position, double size, double lifetime)
        : base(position, size, lifetime)
    {
        sprite.Fill = Brushes.BlueViolet;
        pointsValue = ((1.0 / size) / lifetime) * 1000.0;
    }

    public override string onClick(CPlayer player, CController controller, Point mousePosition)
    {
        if (!isMouseOnObject(mousePosition)) return null;

        double value = pointsValue;
        controller.pointsIncrease(value);
        return $"+{value:0.0} очков";
    }
}