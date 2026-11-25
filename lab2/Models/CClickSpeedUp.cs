using System.Windows;
using System.Windows.Media;

public class CClickSpeedUp : CCollectable
{
    private double modifier;

    public CClickSpeedUp(Point position, double size, double lifetime, double modifier = 0.6)
        : base(position, size, lifetime)
    {
        this.modifier = modifier;
        sprite.Fill = Brushes.Red;
    }

    public override bool onClick(CPlayer player, CController controller, Point mousePosition)
    {
        if (!isMouseOnObject(mousePosition)) return false;
        player.increaseSpeed(modifier);
        return true;
    }
}