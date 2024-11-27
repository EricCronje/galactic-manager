namespace ArchCorpUtilities.Utilities
{
    internal class CodeTemplate(string name, string? code)
    {
        public string Name { get; set; } = name;
        public string? Code { get; set; } = code;
    }
}