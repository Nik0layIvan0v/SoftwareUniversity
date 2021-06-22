namespace CarShop.ViewModels.Cars
{
    public class AddCarInputModel
    {
        public string Model { get; set; } //– a string with min length 5 and max length 20 (required)

        public int Year { get; set; } //– a number (required)

        public string Image { get; set; } // – string (required)

        public string Plate { get; set; } //– a string – Must be a valid Plate number (2 Capital English letters, followed by 4 digits, followed by 2 Capital English letters (required)
    }
}