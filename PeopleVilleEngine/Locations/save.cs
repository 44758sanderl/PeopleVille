using System;
using System.Collections.Generic;
using System.Threading;

class Location
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public List<Resident> Residents { get; set; } = new List<Resident>();

    public Location(string name, int x, int y)
    {
        Name = name;
        X = x;
        Y = y;
    }

    public void AddResident(Resident resident)
    {
        if (!Residents.Contains(resident))
        {
            Residents.Add(resident);
        }
    }

    public void RemoveResident(Resident resident)
    {
        Residents.Remove(resident);
    }

    public double DistanceTo(Location location)
    {
        int x = X - location.X;
        int y = Y - location.Y;

        return Math.Sqrt(x * x + y * y);
    }
}

class Resident
{
    public string Name { get; set; }
    public Location CurrentLocation { get; private set; }
    public Location? Destination { get; private set; }

    public bool IsMoving
    {
        get { return Destination != null; }
    }

    public delegate void ResidentMovement(
        Resident resident,
        Location from,
        Location to
    );

    public event ResidentMovement? MovementStarted;
    public event ResidentMovement? MovementFinished;

    public Resident(string name, Location location)
    {
        Name = name;
        CurrentLocation = location;

        location.AddResident(this);
    }

    public void MoveTo(Location destination)
    {
        if (destination == null)
        {
            throw new ArgumentNullException(nameof(destination));
        }

        if (destination == CurrentLocation)
        {
            Console.WriteLine(Name + " er allerede ved " + destination.Name);
            return;
        }

        if (IsMoving)
        {
            Console.WriteLine(
                Name + " er allerede på vej til " +
                Destination!.Name
            );

            return;
        }

        Destination = destination;

        MovementStarted?.Invoke(
            this,
            CurrentLocation,
            destination
        );
    }

    public void FinishMovement()
    {
        if (Destination == null)
        {
            return;
        }

        Location oldLocation = CurrentLocation;
        Location newLocation = Destination;

        oldLocation.RemoveResident(this);

        CurrentLocation = newLocation;

        newLocation.AddResident(this);

        Destination = null;

        MovementFinished?.Invoke(
            this,
            oldLocation,
            newLocation
        );
    }
}

class MovementManager
{
    private List<Resident> residents;
    private Random random = new Random();

    public MovementManager(List<Resident> residents)
    {
        this.residents = residents;
    }

    public void MoveRandomResidents(List<Location> locations)
    {
        foreach (Resident resident in residents)
        {
            if (resident.IsMoving)
            {
                continue;
            }

            Location destination;

            do
            {
                destination = locations[
                    random.Next(locations.Count)
                ];
            }
            while (destination == resident.CurrentLocation);

            resident.MoveTo(destination);
        }
    }

    public void FinishMovements()
    {
        foreach (Resident resident in residents)
        {
            if (!resident.IsMoving)
            {
                continue;
            }

            double distance =
                resident.CurrentLocation.DistanceTo(
                    resident.Destination!
                );

            int travelTime = Math.Max(
                1,
                (int)Math.Ceiling(distance / 10)
            );

            Console.WriteLine(
                resident.Name +
                " rejser " +
                Math.Round(distance, 1) +
                " enheder. Rejsetid: " +
                travelTime +
                " sek."
            );

            Thread.Sleep(travelTime * 500);

            resident.FinishMovement();
        }
    }
}

class Program
{
    static void Main()
    {
        Location bank = new Location(
            "Bank",
            10,
            20
        );

        Location supermarket = new Location(
            "Supermarked",
            30,
            10
        );

        Location park = new Location(
            "Park",
            50,
            30
        );

        Location hospital = new Location(
            "Hospital",
            70,
            50
        );

        List<Location> locations = new List<Location>
        {
            bank,
            supermarket,
            park,
            hospital
        };

        Resident walt = new Resident(
            "Walt",
            bank
        );

        Resident anna = new Resident(
            "Anna",
            supermarket
        );

        Resident peter = new Resident(
            "Peter",
            park
        );

        List<Resident> residents = new List<Resident>
        {
            walt,
            anna,
            peter
        };

        foreach (Resident resident in residents)
        {
            resident.MovementStarted += MovementStarted;
            resident.MovementFinished += MovementFinished;
        }

        MovementManager movementManager =
            new MovementManager(residents);

        Console.WriteLine("Simulation startet");
        Console.WriteLine();

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(
                "----- Runde " + (i + 1) + " -----"
            );

            movementManager.MoveRandomResidents(
                locations
            );

            movementManager.FinishMovements();

            Console.WriteLine();

            foreach (Resident resident in residents)
            {
                Console.WriteLine(
                    resident.Name +
                    " er ved " +
                    resident.CurrentLocation.Name
                );
            }

            Console.WriteLine();
        }

        Console.WriteLine("Simulation slut.");
    }

    static void MovementStarted(
        Resident resident,
        Location from,
        Location to)
    {
        Console.WriteLine(
            resident.Name +
            " forlader " +
            from.Name +
            " og tager til " +
            to.Name
        );
    }

    static void MovementFinished(
        Resident resident,
        Location from,
        Location to)
    {
        Console.WriteLine(
            resident.Name +
            " ankom til " +
            to.Name
        );
    }
}
