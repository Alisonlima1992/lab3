using System;
using System.Collections.Generic;
using System.Windows;

public class CController
{
    private List<CCollectable> objects = new List<CCollectable>();
    private double spawnRate;
    private double time;
    private Random rng = new Random();
    private double minLifetime = 1.5;
    private double maxLifetime = 4.0;
    private double minSpriteSize = 18;
    private double maxSpriteSize = 32;
    private Size sceneSize;
    private double points = 0;

    public CController(double spawnRate, double startTime, Size sceneSize)
    {
        this.spawnRate = spawnRate;
        this.time = startTime;
        this.sceneSize = sceneSize;
    }

    public void spawnObject()
    {
        double size = minSpriteSize + rng.NextDouble() * (maxSpriteSize - minSpriteSize);
        double lifetime = minLifetime + rng.NextDouble() * (maxLifetime - minLifetime);
        double x = rng.NextDouble() * (sceneSize.Width - size);
        double y = rng.NextDouble() * (sceneSize.Height - size);
        Point pos = new Point(x, y);

        int type = rng.Next(0, 10);

        CCollectable obj;

        if (type < 5)
            obj = new CPointGiver(pos, size, lifetime);
        else if (type < 7)
            obj = new CSpawnRateChanger(pos, size, lifetime);
        else if (type < 9)
            obj = new CLifetimeChanger(pos, size, lifetime);
        else
            obj = new CClickSpeedUp(pos, size, lifetime);

        objects.Add(obj);
    }

    public void destroyObject(CCollectable obj)
    {
        objects.Remove(obj);
    }

    public void update(double delta)
    {
        time += delta;

        if (time >= spawnRate)
        {
            spawnObject();
            time = 0;
        }

        for (int i = objects.Count - 1; i >= 0; i--)
        {
            if (objects[i].updateLifetime(delta))
                destroyObject(objects[i]);
        }
    }

    public void mouseClick(CPlayer player, Point mousePosition)
    {
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            if (objects[i].onClick(player, this, mousePosition))
            {
                destroyObject(objects[i]);
                break;
            }
        }
    }

    public void pointsIncrease(double value) => points += value;

    public void changeSpawnRate(double modifier)
    {
        spawnRate = Math.Max(0.35, spawnRate * modifier);
    }

    public void changeLifetimeRange(double min, double max)
    {
        minLifetime = min;
        maxLifetime = max;
    }

    public double getPoints() => points;
    public IReadOnlyList<CCollectable> getObjects() => objects.AsReadOnly();
}