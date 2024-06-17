namespace RentalSystem.Entities.Vehicles
{
    public class Car : Vehicle
    {
        int safetyRating;

        public Car(string make, string model, decimal valuedAt, int safetyRating) : base(make, model, valuedAt)
        {
            SafetyRating = safetyRating;
        }

        public int SafetyRating
        {
            get => safetyRating;
            private set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException("Safety rating's value is invalid");
                }

                safetyRating = value;
            }
        }
    }
}
