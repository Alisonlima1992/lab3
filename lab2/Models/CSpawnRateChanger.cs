using System.Windows;
using System.Windows.Media;

public class CSpawnRateChanger : CCollectable
{
    private double modifier;

    public CSpawnRateChanger(Point position, double size, double lifetime, double modifier = 0.7)
        : base(position, size, lifetime)
    {
        this.modifier = modifier;
        sprite.Fill = Brushes.HotPink;
    }

    public override string onClick(CPlayer player, CController controller, Point mousePosition)
    {
        if (!isMouseOnObject(mousePosition)) return null;

        controller.changeSpawnRate(modifier);
        return "Частота появления увеличена!";
    }
}