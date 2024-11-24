using System.Text;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

namespace CLILib
{
    public class CLIP : IDisposable
    {
        static StringBuilder? InstructionsSB = new();
        

        public CLIP()
        {
            InstructionsSB = new ();
        }

        private static string? GetInstructions()
        {
            InstructionsSB ??= new();
            InstructionsSB?.AppendLine($"Usage: GenCode -G <Filename> ");
            //InstructionsSB?.AppendLine($"-I <FileName> (GUIDFilename)");
            //InstructionsSB?.AppendLine($"-S <GenCodeWorkingFolder> (Set Working Folder)");
            //InstructionsSB?.AppendLine($"-P <PathToWorkingFolder> (Path to deploy the code to)");
            //InstructionsSB?.AppendLine($"-C (Check to show the working folder P and S - show the files it will check plus the sub paths set. - show Guid used.)");
            //InstructionsSB?.AppendLine($"-D (Deploy the code to working folder for the code to be compiled and used. - part of P)");
            //InstructionsSB?.AppendLine($"-A (To add the filename and the sub path - Part of C, S and P)");
            InstructionsSB?.AppendLine($"-G (Provide the GUID Folder Path and will Generate code in to the working gen folder)");
            //InstructionsSB?.AppendLine($"-M <GuidName> (Set the GUID to use for this session)");
            //InstructionsSB?.AppendLine($"-B Create the structure used (Backup + folders)");
            //InstructionsSB?.AppendLine($"-R (Ready the structure copy the current files setup into the structure as start.)");
            //InstructionsSB?.AppendLine($"-L (Get all the GUIDS from the files)");
            //InstructionsSB?.AppendLine($"-N (Clean the GUIDS - remove all code in between the GUID pairs. -- And make a list of the GUIDS found)");
            var Result = InstructionsSB?.ToString();    
            return Result;
        }

        public static string ProcessArgs(string[] args)
        {
            if (args.Length == 0)
                CH.Feedback(GetInstructions());

            if (args?[0].ToLower() == "-g")
            {
                if (args?[1].Length > 0)
                {
                    return $"Success|GenerateMenu|{args?[1]}";
                }
            }

            if (args?[0].ToLower() == "-r")
            {
                return $"Success|RollBackMenu|None";
            }

            if (args?[0].ToLower() == "-d")
            {
                return $"Success|DeployMenu|None";
            }

            return $"Failed|None|None";
        }

        public void Dispose()
        {
            InstructionsSB?.Clear();
            GC.SuppressFinalize( this );
        }

        public static string ProcessArgsCode(string[] args)
        {
            if (args.Length == 0)
                CH.Feedback(GetInstructions());

            if (args?[0].ToLower() == "-g")
            {
                if (args?[1].Length > 0)
                {
                    return $"Success|GenerateCode|{args?[1]}";
                }
            }

            if (args?[0].ToLower() == "-r")
            {
                return $"Success|RollBack|None";
            }

            if (args?[0].ToLower() == "-d")
            {
                return $"Success|Deploy|None";
            }

            return $"Failed|None|None";
        }
    }
}
