namespace TaxiFleet.Interfaces
{
    interface IEngine
    {
        // Объем, мощность, расход топлива
        float Volume { get; }
        ushort Power { get; }
        double FuelConsumption { get; }
    }
}