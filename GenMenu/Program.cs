using M = GenMenuHelperLib.GenMenuHelperLib;
internal class Program
{
    private static void Main(string[] args)
    {
        using M genCode = new();
        M.ProcessArgs(args, null);
    }
}