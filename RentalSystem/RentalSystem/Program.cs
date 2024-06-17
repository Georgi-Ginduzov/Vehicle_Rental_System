
using RentalSystem.Entities;
using RentalSystem.Entities.Vehicles;
using RentalSystem.Service;

namespace RentalSystem
{
    public class Program
    {
        static void Main()
        {
            // Add vehicles
            Vehicle mitsubishiCar = new Car("Mitsubishi", "Mirage", 15_000, 3);
            Vehicle triumphMotorcycle = new Motorcycle("Triumph", "Tiger Sport 660", 10_000);
            Vehicle citroenCargoVan = new CargoVan("Citroen", "Jumper", 20_000);

            Customer customer1 = new ("John", "1234567890", "john@gmail.com", 20, 2);

            RentalService rentalService = new ();
            rentalService.AddVehicle(mitsubishiCar);
            rentalService.AddCustomer(customer1);

            rentalService.RentVehicle(customer1, mitsubishiCar, new DateTime(2024, 6, 3), new DateTime(2024, 6, 13));
            rentalService.ReturnVehicle(customer1, mitsubishiCar, new DateTime(2024, 6, 13));









            
        }


    }
}
