using System;

class Event
{
    public string EventTitle { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public Address Address { get; set; }

    public Event(string title, string desc, DateTime date, TimeSpan time, Address address)
    {
        EventTitle = title;
        Description = desc;
        Date = date;
        Time = time;
        Address = address;
    }

    public virtual string GenerateStandardMessage()
    {
        return $"Event: {EventTitle}\nDescription: {Description}\nDate: {Date.ToShortDateString()}\nTime: {Time}\nAddress: {Address.GetFullAddress()}";
    }

    public virtual string GenerateFullDetailsMessage()
    {
        return GenerateStandardMessage();
    }

    public virtual string GenerateShortDescription()
    {
        return $"Type: Event, Title: {EventTitle}, Date: {Date.ToShortDateString()}";
    }
}

class Lecture : Event
{
    public string Speaker { get; set; }
    public int Capacity { get; set; }

    public Lecture(string title, string desc, DateTime date, TimeSpan time, Address address, string speaker, int capacity)
        : base(title, desc, date, time, address)
    {
        Speaker = speaker;
        Capacity = capacity;
    }

    public override string GenerateFullDetailsMessage()
    {
        return base.GenerateFullDetailsMessage() + $"\nSpeaker: {Speaker}\nCapacity: {Capacity}";
    }
}

class Reception : Event
{
    public string RSVP { get; set; }

    public Reception(string title, string desc, DateTime date, TimeSpan time, Address address, string rsvp)
        : base(title, desc, date, time, address)
    {
        RSVP = rsvp;
    }

    public override string GenerateFullDetailsMessage()
    {
        return base.GenerateFullDetailsMessage() + $"\nRSVP: {RSVP}";
    }
}

class OutdoorGathering : Event
{
    public string WeatherForecast { get; set; }

    public OutdoorGathering(string title, string desc, DateTime date, TimeSpan time, Address address, string forecast)
        : base(title, desc, date, time, address)
    {
        WeatherForecast = forecast;
    }

    public override string GenerateFullDetailsMessage()
    {
        return base.GenerateFullDetailsMessage() + $"\nWeather Forecast: {WeatherForecast}";
    }
}

class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {State}, {Country}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address address = new Address { Street = "456 Elm St", City = "Othertown", State = "NY", Country = "USA" };

        Event lecture = new Lecture("Lecture Title", "Lecture", DateTime.Now, TimeSpan.FromHours(2), address, "John Doe", 50);
        Event reception = new Reception("Reception Title", "Reception", DateTime.Now, TimeSpan.FromHours(3), address, "RSVP@mail.com");
        Event outdoorGathering = new OutdoorGathering("Outdoor Gathering Title", "Outdoor Gathering", DateTime.Now, TimeSpan.FromHours(4), address, "Sunny");

        Console.WriteLine("Standard Message:");
        Console.WriteLine(lecture.GenerateStandardMessage());
        Console.WriteLine(reception.GenerateStandardMessage());
        Console.WriteLine(outdoorGathering.GenerateStandardMessage());

        Console.WriteLine("\nFull Details Message:");
        Console.WriteLine(lecture.GenerateFullDetailsMessage());
        Console.WriteLine(reception.GenerateFullDetailsMessage());
        Console.WriteLine(outdoorGathering.GenerateFullDetailsMessage());

        Console.WriteLine("\nShort Description:");
        Console.WriteLine(lecture.GenerateShortDescription());
        Console.WriteLine(reception.GenerateShortDescription());
        Console.WriteLine(outdoorGathering.GenerateShortDescription());
    }
}
