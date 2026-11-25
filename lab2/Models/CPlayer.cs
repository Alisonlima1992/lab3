using System;
using System.Windows;

public class CPlayer
{
    private bool canClick = true;
    private double timeBeforeClick;
    private CCountdownTimer countdownTimer;
    private double damage = 10;
    private double gold = 0;

    public CPlayer(double timeBeforeClick)
    {
        this.timeBeforeClick = timeBeforeClick;
        countdownTimer = new CCountdownTimer(timeBeforeClick);
    }

    public void update(double delta)
    {
        countdownTimer.update(delta);
        if (countdownTimer.isReady())
            canClick = true;
    }

    public bool TryClick()
    {
        if (!canClick) return false;

        canClick = false;
        countdownTimer.reset();
        return true;
    }

    public void increaseSpeed(double speedModifier)
    {
        timeBeforeClick = Math.Max(0.08, timeBeforeClick * speedModifier);
        countdownTimer.setTarget(timeBeforeClick);
    }

    public bool TryUpgradeCooldown(double cost)
    {
        if (gold < cost) return false;

        gold -= cost;
        timeBeforeClick = Math.Max(0.08, timeBeforeClick * 0.85);
        countdownTimer.setTarget(timeBeforeClick);
        return true;
    }

    public void AddGold(double amount) => gold += amount;
    public void AddDamage(double amount) => damage += amount;

    public double Damage => damage;
    public double Gold => gold;
    public bool CanClick => canClick;
    public double Cooldown => timeBeforeClick;
}