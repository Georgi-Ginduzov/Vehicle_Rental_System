using RentalSystem.Entities;
using RentalSystem.Entities.Vehicles;

namespace RentalSystem.RentalService
{
    public class RentalService
    {
        private HashSet<Customer> customers;
        private HashSet<Vehicle> vehicles;
        private SortedSet<RentalInfo> rentalInfos;

        public RentalService()
        {
            customers = new HashSet<Customer>();
            vehicles = new HashSet<Vehicle>();
            rentalInfos = new SortedSet<RentalInfo>();
        }

        public RentalService(IEnumerable<Vehicle> vehicles)
        {
            this.vehicles = new HashSet<Vehicle>(vehicles);
            customers = new();
            rentalInfos = new();
        }

        public IReadOnlyCollection<Customer> Customers => customers ??= new HashSet<Customer>();
        public IReadOnlyCollection<Vehicle> Vehicles => vehicles ??= new HashSet<Vehicle>();


        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public void RemoveCustomer(Customer customer)
        {
            customers.Remove(customer);
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
        }


        public bool RentVehicle(Customer customer, Vehicle vehicle, DateTime ReservationStartDate, DateTime ReservationEndDate)
        {
            try
            {
                _ = IsRentingPossible(customer, vehicle, ReservationStartDate, ReservationEndDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Reservation is not possible: " + ex.Message);
                return false;
            }

            rentalInfos.Add(new RentalInfo(customer, vehicle, ReservationStartDate, ReservationEndDate));
            
            customers.Remove(customer);
            vehicles.Remove(vehicle);

            return true;
        }

        public bool ReturnVehicle(Customer customer, Vehicle vehicle, DateTime actualReturnDate)
        {
            var rentalInfo = rentalInfos.FirstOrDefault(r => r.Customer.Equals(customer) && r.Vehicle.Equals(vehicle));

            if (rentalInfo == null)
            {
                Console.WriteLine("There isn't such rental in our system");
                return false;
            }

            try
            {
                IsReturningPossible(rentalInfo, actualReturnDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }



            rentalInfos.Remove(rentalInfo);
            customers.Add(customer);
            vehicles.Add(vehicle);

            return true;
        }

        private bool IsRentingPossible(Customer customer, Vehicle vehicle, DateTime ReservationStartDate, DateTime ReservationEndDate)
        {
            if (!customers.Contains(customer))
            {
                customers.Add(customer);
            }

            if (!vehicles.Contains(vehicle))
            {
                throw new ArgumentOutOfRangeException(" There is no such vehicle");
            }

            if (ReservationStartDate < DateTime.Now || ReservationStartDate > ReservationEndDate)
            {
                throw new ArgumentOutOfRangeException("Reservation start date is invalid");
            }

            return true;
        }

        private bool IsReturningPossible(RentalInfo rentalInfo, DateTime actualReturnDate)
        {
            if (!customers.Contains(rentalInfo.Customer))
            {
                throw new ArgumentOutOfRangeException("There is no such customer");
            }

            if (!vehicles.Contains(rentalInfo.Vehicle))
            {
                throw new ArgumentOutOfRangeException("There is no such vehicle");
            }

            if (actualReturnDate < rentalInfo.RentalStartDate)
            {
                throw new ArgumentOutOfRangeException("Invalid actual return date. Cannot return");
            }

            return true;
        }

        private void UpdateRentalInfo(RentalInfo rentalInfo, DateTime actualReturnDate)
        {
            rentalInfo.ActualReturnDate = actualReturnDate;
            
        }
    }
}
