using System;
using System.Collections.Generic;

class Activity
{
    public DateTime Date { get; set; }
    public int LengthInMinutes { get; set; }

    public Activity(DateTime date, int length)
    {
        Date = date;
        LengthInMinutes = length;
    }

    public virtual double GetDistance()
    {
        return 0;
    }

    public virtual double GetSpeed()
    {
        return 0;
    }

    public virtual double GetPace()
    {
        return 0;
    }

    public virtual string GetSummary()
    {
        return $"{Date.ToShortDateString()} {GetType().Name} ({LengthInMinutes} min)";
    }
}

class Running : Activity
{
    public double Distance { get; set; }

    public Running(DateTime date, int length, double distance)
        : base(date, length)
    {
        Distance = distance;
    }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return Distance / (LengthInMinutes / 60.0);
    }

    public override double GetPace()
    {
        return LengthInMinutes / Distance;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distance: {Distance} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min/mile";
    }
}

class StationaryBicycle : Activity
{
    public double Speed { get; set; }

    public StationaryBicycle(DateTime date, int length, double speed)
        : base(date, length)
    {
        Speed = speed;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60 / Speed;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Speed: {Speed} mph, Pace: {GetPace()} min/mile";
    }
}

class Swimming : Activity
{
    public int Laps { get; set; }

    public Swimming(DateTime date, int length, int laps)
        : base(date, length)
    {
        Laps = laps;
    }

    public override double GetDistance()
    {
        return Laps * 50 / 1000.0;
    }

    public override double GetSpeed()
    {
        return GetDistance() / (LengthInMinutes / 60.0);
    }

    public override double GetPace()
    {
        return LengthInMinutes / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distance: {GetDistance()} km, Speed: {GetSpeed()} kph, Pace: {GetPace()} min/km";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0));
        activities.Add(new StationaryBicycle(new DateTime(2022, 11, 3), 30, 20));
        activities.Add(new Swimming(new DateTime(2022, 11, 3), 30, 10));

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
