using RentalSystem.Entities;
using RentalSystem.Entities.Vehicles;

namespace RentalSystem.Service
{
    public static class RentalServiceCalculator
    {
        public static int CalculateRentalPrice(RentalInfo rentalInfo)
        {
            int rentalCost = 0;

            if (rentalInfo.ActualRentalDays <= 7)
            {
                switch (rentalInfo.Vehicle)
                {
                    case Car:
                        rentalCost = 20;
                        break;
                    case Motorcycle:
                        rentalCost = 15;
                        break;
                    case CargoVan:
                        rentalCost = 50;
                        break;
                }                
            }

            switch (rentalInfo.Vehicle)
            {
                case Car:
                    rentalCost = 15;
                    break;
                case Motorcycle:
                    rentalCost = 10;
                    break;
                case CargoVan:
                    rentalCost = 40;
                    break;
            }

            return rentalCost;
        }

        public static decimal CalculateInsurancePrice(RentalInfo rentalInfo)
        {
            decimal insuranceCost = 0;
            int rentalDays = rentalInfo.ActualRentalDays;
            decimal initialInsuranceCost = 0;
            decimal additionalInsuranceCost = 0;
            Vehicle vehicle = rentalInfo.Vehicle;
            Customer customer = rentalInfo.Customer;

            switch (vehicle)
            {
                case Car car:
                    initialInsuranceCost = car.ValuedAt * 0.01m * rentalDays;
                    additionalInsuranceCost = CalculateAdditionalCarInsurance(car, rentalDays);
                    break;
                case Motorcycle motorcycle:
                    initialInsuranceCost = motorcycle.ValuedAt * 0.02m * rentalDays;
                    additionalInsuranceCost = CalculateMotorcycleAdditionalInsurance(motorcycle, customer, rentalDays);
                    break;
                case CargoVan cargoVan:
                    initialInsuranceCost = cargoVan.ValuedAt * 0.03m * rentalDays;
                    additionalInsuranceCost = CalculateCargoVanAdditionalInsurance(cargoVan, customer, rentalDays);
                    break;
            }

            rentalInfo.UpdateInsuranceDetails(initialInsuranceCost, additionalInsuranceCost);

            return insuranceCost;
        }

        private static decimal CalculateAdditionalCarInsurance(Car car, int rentalDays)
        {
            decimal initialInsuranceCost = car.ValuedAt * 0.01m * rentalDays;
            decimal additionalInsurance = 0;

            if (car.SafetyRating >= 4)
            {
                initialInsuranceCost *= 0.9m;
                additionalInsurance = -(initialInsuranceCost * 0.1m);
            }
            return additionalInsurance;
        }

        private static decimal CalculateMotorcycleAdditionalInsurance(Motorcycle motorcycle, Customer customer, int rentalDays)
        {
            decimal initialInsuranceCost = motorcycle.ValuedAt * 0.02m * rentalDays;
            decimal additionalInsurance = 0; 

            if (customer.Age < 25)
            {
                initialInsuranceCost *= 1.2m; 
                additionalInsurance = initialInsuranceCost * 0.2m;
            }
            return additionalInsurance;
        }

        private static decimal CalculateCargoVanAdditionalInsurance(CargoVan cargoVan, Customer customer, int rentalDays)
        {
            decimal initialInsuranceCost = cargoVan.ValuedAt * 0.03m * rentalDays;
            decimal additionalInsurance = 0;
            if (customer.DrivingExperience > 5)
            {
                initialInsuranceCost *= 0.85m;
                additionalInsurance = -(initialInsuranceCost * 0.15m);
            }
            return additionalInsurance;
        }
    }
}
