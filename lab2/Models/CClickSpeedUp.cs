using System.Windows;
using System.Windows.Media;

public class CClickSpeedUp : CCollectable
{
    private double speedModifier;

    public CClickSpeedUp(Point position, double size, double lifetime, double speedModifier = 0.5)
        : base(position, size, lifetime)
    {
        this.speedModifier = speedModifier;
        sprite.Fill = Brushes.Red;
    }

    public override bool onClick(CPlayer player, CController controller, Point mousePosition)
    {
        if (!isMouseOnObject(mousePosition)) return false;
        player.increaseSpeed(speedModifier);
        return true;
    }
}