using System;
using System.Collections.Generic;
using System.Windows;

public class CController
{
    private List<CObject> objects = new List<CObject>();
    private double spawnRate;
    private double time;
    private Random rng = new Random();
    private double minLifetime = 1.0;
    private double maxLifetime = 5.0;
    private double minSpriteSize = 10.0;
    private double maxSpriteSize = 20.0;
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

        objects.Add(new CObject(new Point(x, y), size, lifetime));
    }

    public void destroyObject(CObject obj)
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

    public void mouseClick(Point mousePosition)
    {
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            if (objects[i].isMouseOnObject(mousePosition))
            {
                points += objects[i].getPointsValue();
                destroyObject(objects[i]);
                break;
            }
        }
    }

    public void pointsIncrease(double value)
    {
        points += value;
    }

    public void changeSpawnRate(double modifier)
    {
        spawnRate = Math.Max(0.3, spawnRate * modifier);
    }

    public void changeLifetimeRange(double min, double max)
    {
        minLifetime = min;
        maxLifetime = max;
    }

    public double getPoints() => points;
    public IReadOnlyList<CObject> getObjects() => objects.AsReadOnly();
}