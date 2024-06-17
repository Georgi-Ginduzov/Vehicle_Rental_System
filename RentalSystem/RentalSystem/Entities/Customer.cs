using RentalSystem.Entities.Helpers;

namespace RentalSystem.Entities
{
    public sealed class Customer
    {
        private string? name;
        private string? phoneNumber;
        private string? email;
        private int age;
        private int drivingExperience;

        public Customer(string? name, string? phoneNumber, string? email, int age, int drivingExperience)
        {
            CustomerId = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Age = age;
            DrivingExperience = drivingExperience;
        }

        public Guid CustomerId { get; private set; }
        public string? Name 
        {
            get => name;
            set
            {
                if (value != null)
                {
                    name = value;
                }
            }
        }
        public string? PhoneNumber 
        { 
            get => phoneNumber;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(PhoneNumber), "Phone number cannot be null");
                }

                if (!Validator.IsValidPhoneNumber(value))
                {
                    throw new ArgumentException("Phone number is invalid");
                }

                phoneNumber = value;
            }
        }
        public string? Email 
        {
            get => email;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Email), "Email cannot be null");
                }

                if (!Validator.IsValidEmail(value))
                {
                    throw new ArgumentException("Email is invalid");
                }
                email = value;                
            }
        }

        public int Age 
        { 
            get => age;
            set
            {
                if (value < 18)
                {
                    throw new ArgumentException("Customer must be at least 18 years old");
                }

                age = value;
            }
        }

        public int DrivingExperience 
        { 
            get => drivingExperience;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Driving experience cannot be negative");
                }

                drivingExperience = value;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Object should not be null during comparisson with " + GetType());
            }


            if (obj is Customer otherCustomer)
            {
                return CustomerId == otherCustomer.CustomerId;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerId);
        }
    }
}
