namespace ArchCorpUtilities.Utilities
{
    internal class CodeTemplate
    {
        public CodeTemplate(string name, string? code)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; set; }
        public string? Code { get; set; }
    }
}