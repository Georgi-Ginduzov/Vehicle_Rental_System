using RentalSystem.Entities.Helpers;

namespace RentalSystem.Entities
{
    public class Customer
    {
        private string? name;
        public string? phoneNumber;
        public string? email;

        public Customer(string? name, string? phoneNumber, string? email)
        {
            CustomerId = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
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

        

    }
}
