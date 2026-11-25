using System.Windows;
using System.Windows.Media;

public class CDamageBoost : CCollectable
{
    private double boost;

    public CDamageBoost(Point position, double size, double lifetime, double boost = 5)
        : base(position, size, lifetime)
    {
        this.boost = boost;
        sprite.Fill = Brushes.OrangeRed;
    }

    public override string onClick(CPlayer player, CController controller, Point mousePosition)
    {
        if (!isMouseOnObject(mousePosition)) return null;

        player.AddDamage(boost);
        return $"+{boost} урона";
    }
}