namespace RentalSystem.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal RentalRate { get; set; } // Calculate it in the business logic
        public bool IsAvailable { get; set; } // Make a set in the vehicles array


        public Vehicle(string make, string model, decimal rentalRate)
        {
            Id = vehicleId;
            Make = make;
            Model = model;
            RentalRate = rentalRate;
            IsAvailable = true;
        }

        /*public void AddMaintenanceRecord(Maintenance maintenance)
        {
            MaintenanceRecords.Add(maintenance);
        }*/
    }
}
