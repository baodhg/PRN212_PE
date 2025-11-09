using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ChargingSession
{
    public int SessionId { get; set; }

    public string SessionTitle { get; set; } = null!;

    public string ChargingStation { get; set; } = null!;

    public TimeOnly BeginTime { get; set; }

    public TimeOnly FinishTime { get; set; }

    public int? VehicleId { get; set; }

    public decimal ChargingFee { get; set; }

    public virtual ElectricVehicle? Vehicle { get; set; }
}
