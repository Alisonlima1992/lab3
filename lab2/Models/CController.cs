using System;
using System.Collections.Generic;
using System.Windows;

public class CController
{
    private List<CCollectable> objects = new List<CCollectable>();
    private double spawnRate = 1.8;
    private double time = 0;
    private Random rng = new Random();
    private double minLifetime = 2.0;
    private double maxLifetime = 4.5;
    private double minSpriteSize = 22;
    private double maxSpriteSize = 38;
    private Size sceneSize;

    public CController(Size sceneSize)
    {
        this.sceneSize = sceneSize;
    }

    public void spawnObject()
    {
        double size = minSpriteSize + rng.NextDouble() * (maxSpriteSize - minSpriteSize);
        double lifetime = minLifetime + rng.NextDouble() * (maxLifetime - minLifetime);

        double x = sceneSize.Width * 0.25 + rng.NextDouble() * (sceneSize.Width * 0.5);
        double y = sceneSize.Height * 0.35 + rng.NextDouble() * (sceneSize.Height * 0.4);

        Point pos = new Point(x, y);
        int type = rng.Next(0, 12);

        CCollectable obj;

        if (type < 4)
            obj = new CPointGiver(pos, size, lifetime);
        else if (type < 6)
            obj = new CGoldBoost(pos, size, lifetime);
        else if (type < 8)
            obj = new CDamageBoost(pos, size, lifetime);
        else if (type < 10)
            obj = new CClickSpeedUp(pos, size, lifetime);
        else
            obj = new CSpawnRateChanger(pos, size, lifetime);

        objects.Add(obj);
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

    public string mouseClick(CPlayer player, Point mousePosition)
    {
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            string result = objects[i].onClick(player, this, mousePosition);
            if (result != null)
            {
                destroyObject(objects[i]);
                return result;
            }
        }
        return null;
    }

    public void destroyObject(CCollectable obj) => objects.Remove(obj);

    public void changeSpawnRate(double modifier)
    {
        spawnRate = Math.Max(0.4, spawnRate * modifier);
    }

    public void changeLifetimeRange(double min, double max)
    {
        minLifetime = min;
        maxLifetime = max;
    }

    public void pointsIncrease(double value)
    {
    }

    public IReadOnlyList<CCollectable> getObjects() => objects.AsReadOnly();
}