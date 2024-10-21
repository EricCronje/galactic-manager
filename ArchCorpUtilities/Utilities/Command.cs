namespace ArchCorpUtilities.Utilities
{
    public class Command
    {
        //POCO - Plain Old CLR Objects
        public int? Choice { get; set; }
        public string? Value { get; set; }

        public Command(int? choice, string? value)
        {
            Value = value;
            Choice = choice;
        }
    }
}