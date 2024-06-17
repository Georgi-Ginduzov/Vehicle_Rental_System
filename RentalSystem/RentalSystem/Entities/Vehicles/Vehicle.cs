using RentalSystem.Entities.Helpers;

namespace RentalSystem.Entities.Vehicles
{
    public abstract class Vehicle
    {
        decimal valuedAt;

        public Vehicle(string make, string model, decimal valuedAt)
        {
            Id = Guid.NewGuid();
            Make = make;
            Model = model;
            ValuedAt = valuedAt;
        }

        public Guid Id { get; private set; }
        public string Make { get; private set; }
        public string Model { get; private set; }
        public decimal ValuedAt
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

        public override bool Equals(object? obj)
        {
            return obj is Vehicle vehicle &&
                   Id.Equals(vehicle.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
