using TaxiFleet.Enum;

namespace TaxiFleet.Interfaces
{
    interface IBody
    {
        // Тип кузова, количество дверей, количество мест, объем багажника
        BodyTypes BodyType { get; }
        byte NumberOfDoors { get; }
        byte NumberOfSeats { get; }
        float TrunkVolume { get; }
    }
}