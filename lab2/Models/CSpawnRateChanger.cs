using System.Windows;
using System.Windows.Media;

public class CSpawnRateChanger : CCollectable
{
    private double speedModifier;

    public CSpawnRateChanger(Point position, double size, double lifetime, double speedModifier = 0.7)
        : base(position, size, lifetime)
    {
        this.speedModifier = speedModifier;
        sprite.Fill = Brushes.HotPink;
    }

    public override bool onClick(CPlayer player, CController controller, Point mousePosition)
    {
        if (!isMouseOnObject(mousePosition)) return false;
        controller.changeSpawnRate(speedModifier);
        return true;
    }
}