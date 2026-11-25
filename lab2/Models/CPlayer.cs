using System;
using System.Windows;

public class CPlayer
{
    private bool canClick = true;
    private double timeBeforeClick;
    private CCountdownTimer countdownTimer;

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

    public void mouseClick(Point mousePosition)
    {
        if (!canClick) return;
        canClick = false;
        countdownTimer.reset();
    }

    public void increaseSpeed(double speedModifier)
    {
        timeBeforeClick = Math.Max(0.12, timeBeforeClick * speedModifier);
        countdownTimer.setTarget(timeBeforeClick);
    }

    public bool CanClick => canClick;
}