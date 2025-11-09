using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ElectricVehicle
{
    public int VehicleId { get; set; }

    public string ModelName { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string BatteryCapacity { get; set; } = null!;

    public virtual ICollection<ChargingSession> ChargingSessions { get; set; } = new List<ChargingSession>();
}
