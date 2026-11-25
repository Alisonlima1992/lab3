public class CCountdownTimer
{
    private double targetTime;
    private double currentTime;

    public CCountdownTimer(double targetTime)
    {
        this.targetTime = targetTime;
        this.currentTime = 0;
    }

    public double getTime() => currentTime;

    public void update(double delta)
    {
        currentTime += delta;
    }

    public bool isReady() => currentTime >= targetTime;

    public void reset() => currentTime = 0;

    public void setTarget(double newTarget) => targetTime = newTarget;
}