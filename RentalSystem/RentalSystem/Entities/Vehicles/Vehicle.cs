using RentalSystem.Entities.Helpers;

namespace RentalSystem.Entities.Vehicles
{
    public abstract class Vehicle
    {
        float valuedAt;

        public Vehicle(string make, string model, float valuedAt)
        {
            Id = Guid.NewGuid();
            Make = make;
            Model = model;
            ValuedAt = valuedAt;
        }

        public Guid Id { get; private set; }
        public string Make { get; private set; }
        public string Model { get; private set; }
        public float ValuedAt
        {
            get => valuedAt;
            set
            {
                if (!Validator.IsValidPrice(value))
                {
                    throw new InvalidOperationException(GetType().ToString() +  "'s price is invalid");
                }

                valuedAt = value;
            }
        }

        /*public void AddMaintenanceRecord(Maintenance maintenance)
        {
            MaintenanceRecords.Add(maintenance);
        }*/
    }
}
