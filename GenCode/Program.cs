using System.Text;
using L = Logger.Logger;
internal class Program
{
    
    private static void Main(string[] args)
    {

        using GenCodeLib.GenCodeLib genCode = new();
        genCode.ProcessCode(args);
    }
}