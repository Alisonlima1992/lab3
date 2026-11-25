using System.Windows;
using System.Windows.Media;

public class CGoldBoost : CCollectable
{
    private double goldAmount;

    public CGoldBoost(Point position, double size, double lifetime, double goldAmount = 25)
        : base(position, size, lifetime)
    {
        this.goldAmount = goldAmount;
        sprite.Fill = Brushes.Gold;
    }

    public override string onClick(CPlayer player, CController controller, Point mousePosition)
    {
        if (!isMouseOnObject(mousePosition)) return null;

        player.AddGold(goldAmount);
        return $"+{goldAmount} золота";
    }
}