﻿
using System.Text;
using L = Logger.Logger;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    public abstract class CodePart
    {
        
        public CodePart(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID)
        {
            BaseFolder = baseFolder;
            TargetFile = targetFile;
            Entity = entity;
            SearchString = searchString;
            WorkingFolder = workingFolder;
            Heading = heading;
            Tabs = searchStringPostPart;
            SessionID = sessionID;
        }

        public string BaseFolder { get; }
        public string TargetFile { get; }
        public string Entity { get; }
        public string SearchString { get; }
        public string WorkingFolder { get; }
        public string Heading { get; }
        public string Tabs { get; }
        public string SessionID { get; }

        public bool AlterCode()
        {
            var TargetPath = $"{WorkingFolder}{BaseFolder}\\{TargetFile}";

            try
            {
                if (File.Exists(TargetPath))
                {
                    var Context = File.ReadAllText(TargetPath);
                    if (string.IsNullOrWhiteSpace(Context))
                        return false;
                    else
                    {
                        var SelectedContext = Context.Split($"//{SearchString}");
                        if (SelectedContext.Length is > 0 and 3)
                        {
                            var CodeToAlter = SelectedContext[1];
                            // Only alter the code if the entity in question does not exist.
                            if (!CodeToAlter.Contains(Entity))
                            {
                                string AlteredCode = ModifyCode(CodeToAlter);
                                if (AlteredCode != "<NoAction>")
                                { 
                                    string AlteredFile = ConcatinateCode(SelectedContext, AlteredCode);
                                    File.WriteAllText(TargetPath, AlteredFile);
                                }
                            }
                        }
                        else
                            L.Log($"Resource.CouldNotLocateTheSearchKey - {SearchString}", SessionID, 9);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal virtual string ModifyCode(string CodeToAlter)
        {
            throw new NotImplementedException();
        }

        private string ConcatinateCode(string[] SelectedContext, string codeAltered)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Clear();

            if(Heading.Length > 0)
                stringBuilder.Append(Heading);

            stringBuilder.Append(SelectedContext[0]);
            stringBuilder.AppendLine($"//{SearchString}");
            stringBuilder.Append(codeAltered);
            stringBuilder.AppendLine($"{Tabs}//{SearchString}");
            stringBuilder.Append(SelectedContext[2].AsSpan(2)); // remove the linefeed
            var AlteredFile = stringBuilder.ToString();
            stringBuilder.Clear();
            return AlteredFile;
        }

    }
}