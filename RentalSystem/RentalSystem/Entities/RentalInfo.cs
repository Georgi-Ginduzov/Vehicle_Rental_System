using RentalSystem.Entities.Vehicles;
using System.Text;

namespace RentalSystem.Entities
{
    public sealed class RentalInfo : IComparable<RentalInfo>
    {
        public Customer Customer { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public DateTime RentalStartDate { get; private set; }
        public DateTime PlannedReturnDate { get; private set; }
        public DateTime ActualReturnDate { get; set; }
        public int PlannedRentalDays => PlannedReturnDate.Subtract(RentalStartDate).Days;
        public int ActualRentalDays => ActualReturnDate.Subtract(RentalStartDate).Days;
        public decimal TotalRental { get; set; }
        public decimal InsuranceRate { get; set; }
        public decimal InsuranceAdditionRate { get; set; }

        public RentalInfo(Customer customer, Vehicle vehicle, DateTime rentalStartDate, DateTime plannedReturnDate)
        {
            Customer = customer;
            Vehicle = vehicle;
            RentalStartDate = rentalStartDate;
            PlannedReturnDate = plannedReturnDate;
        }

        public void UpdateInsuranceDetails(decimal insuranceRate, decimal insuranceAdditionRate)
        {
            InsuranceRate = insuranceRate;
            InsuranceAdditionRate = insuranceAdditionRate;
        }

        public bool Equals(RentalInfo other)
        {
            if (Customer.Equals(other.Customer) && Vehicle.Equals(other.Vehicle))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Customer, Vehicle);
        }

        public int CompareTo(RentalInfo? other)
        {
            if ( other == null)
            {
                throw new ArgumentNullException("Object should not be null during comparisson with " + GetType());
            }

            int returnDateComparison = PlannedReturnDate.CompareTo(other.PlannedReturnDate);

            if (returnDateComparison > 0)
            {
                return 1;
            }
            else if (returnDateComparison < 0)
            {
                return -1;
            }
            else
            {
                return RentalStartDate.CompareTo(other.RentalStartDate);
            }

        }

        public override string ToString()
        {
            decimal insuranceRatePerDay = InsuranceRate / ActualRentalDays;
            decimal additionalInsuranceRatePerDay = InsuranceAdditionRate / ActualRentalDays;
            decimal totalInsuranceRate = insuranceRatePerDay + additionalInsuranceRatePerDay;
            decimal total = TotalRental + totalInsuranceRate;

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("XXXXXXXXXX");
            builder.AppendLine("Date: " + ActualReturnDate.ToString());
            builder.AppendLine("Customer Name: " + Customer.Name);
            builder.AppendLine("Rented Vehicle: " + Vehicle.Make + " " + Vehicle.Model);
            builder.AppendLine();
            builder.AppendLine("Reservation start date: " + RentalStartDate.ToString());
            builder.AppendLine("Reservation end date: " + PlannedReturnDate.ToString());
            builder.AppendLine("Reserved rental days: " + PlannedRentalDays);
            builder.AppendLine();
            builder.AppendLine("Actual return date: " + ActualReturnDate.ToString());
            builder.AppendLine("Actual rentral days: " + ActualRentalDays + " days");
            builder.AppendLine();
            builder.AppendLine("Rental cost per day: $" + TotalRental / ActualRentalDays);
            builder.AppendLine($"Initial insurance per day: ${insuranceRatePerDay}");
            
            if (InsuranceAdditionRate > 0)
            {
                builder.AppendLine($"Insurance addition per day: ${additionalInsuranceRatePerDay}");
            }
            else if (InsuranceAdditionRate < 0)
            {
                builder.AppendLine($"Insurance discount per day: ${additionalInsuranceRatePerDay}");
            }
            
            builder.AppendLine($"Insurance per day: ${(insuranceRatePerDay + additionalInsuranceRatePerDay):F2}");
            builder.AppendLine();
            builder.AppendLine($"Total rent: ${TotalRental:F2}");
            builder.AppendLine($"Total insurance: ${totalInsuranceRate:F2}");
            builder.AppendLine($"Total: ${total:F2}");
            builder.AppendLine("XXXXXXXXXX");

            return builder.ToString();
        }
    }
}
