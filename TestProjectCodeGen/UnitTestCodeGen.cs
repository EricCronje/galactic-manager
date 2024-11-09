using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System.Text;
using CodeGen = ArchCorpUtilities.Utilities.CodeGenHelper;

namespace TestProjectCodeGen
{
    public class UnitTestCodeGen
    {
        [Fact]
        public void ValidTestGenCode_GetBackup_Create_AlphaPrime()
        {
            TestGenCode(true, "AlphaPrime");
            RemoveCreatedModelFiles("AlphaPrime");
            RestoreOriginalModels();
        }

        private void RestoreOriginalModels()
        {
            var UniversalUtilitiesFile = $"{CodeGen.WorkingFolder}\\Utilities\\UniversalUtilities.cs";
            var UniversalUtilitiesFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Utilities\\UniversalUtilities.cs";

            var ArchLoaderFile = $"{CodeGen.WorkingFolder}\\Models\\ArchLoader.cs";
            var ArchLoaderFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Models\\ArchLoader.cs";

            var TargetTaskHelperFile = $"{CodeGen.WorkingFolder}\\Models\\TargetTaskHelper.cs";
            var TargetTaskHelperFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Models\\TargetTaskHelper.cs";

            if (File.Exists(UniversalUtilitiesFile))
                File.Delete(UniversalUtilitiesFile);

            if (File.Exists(TargetTaskHelperFile))
                File.Delete(TargetTaskHelperFile);

            if (File.Exists(ArchLoaderFile))
                File.Delete(ArchLoaderFile);

            if (File.Exists(UniversalUtilitiesFileBackup))
                File.Copy(UniversalUtilitiesFileBackup, UniversalUtilitiesFile);

            if (File.Exists(ArchLoaderFileBackup))
                File.Copy(ArchLoaderFileBackup, ArchLoaderFile);

            if (File.Exists(TargetTaskHelperFileBackup))
                File.Copy(TargetTaskHelperFileBackup, TargetTaskHelperFile);
        }

        [Fact]
        public void ValidTestGenCode_NoBackup_Create_Brent_Charlie()
        {
            TestGenCode(true, "Brent");
            TestGenCode(false, "Charlie");
            var UniversalUtilitiesFile = $"{CodeGen.WorkingFolder}\\Utilities\\UniversalUtilities.cs";
            var ArchLoaderFile = $"{CodeGen.WorkingFolder}\\Models\\ArchLoader.cs";
            var TargetTaskHelperFile = $"{CodeGen.WorkingFolder}\\Models\\TargetTaskHelper.cs";
            var Context = File.ReadAllText(UniversalUtilitiesFile);
            Assert.Contains("Brent", Context); //Find the GUID marker.           
            Assert.Contains("Charlie", Context); //Find the entry point

            Assert.Contains("//{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", Context); //Find the GUID marker.           
            Assert.Contains("//{E401C6FC-99B7-41B0-A612-8DABFE8734C3}", Context); //Find the entry point

            Context = File.ReadAllText(ArchLoaderFile);
            Assert.Contains("Brent", Context); //Find the GUID marker.           
            Assert.Contains("Charlie", Context); //Find the entry point

            Assert.Contains("//{0ACDC688-3120-452F-94AE-2DD1771A9991}", Context); //Using GUID marker
            Assert.Contains("//{048A4DD6-2F1B-4178-A732-E3B50D3F0791}", Context); //public variable Helper GUID marker
            Assert.Contains("//{9ED7AF33-DE0E-45C3-821F-4669558AD744}", Context); //Instantiation of the Helper variable -  GUID marker


            Context = File.ReadAllText(TargetTaskHelperFile);
            Assert.Contains("//{24D86755-6962-4074-BD9F-73E8FE0A5F68}", Context); //Using - GUID marker
            Assert.Contains($"using ArchCorpUtilities.GeneratedModels.BrentModel;", Context); //Using - GUID marker
            Assert.Contains($"using ArchCorpUtilities.GeneratedModels.CharlieModel;", Context); //Using - GUID marker

            Assert.Contains("//{2D8B5F74-6CC5-4C0F-AB99-8E596C463DA0}", Context); //NoData - GUID marker
            Assert.Contains("if (A.BrentHelper != null && !A.BrentHelper.IsItemsOnThePage())", Context); //Using - GUID marker
            Assert.Contains("if (A.CharlieHelper != null && !A.CharlieHelper.IsItemsOnThePage())", Context); //Using - GUID marker

            Assert.Contains("//{EA6AE6CA-7E51-43DE-95F9-FF66E27AE130}", Context); //ArchLoader using - GUID marker
            Assert.Contains("using A = ArchCorpUtilities.Models.ArchLoader;", Context); //Using - GUID marker

            Assert.Contains("//{A6E3C6F1-D649-45CE-8C05-3A87466618A9}", Context); // 
            Assert.Contains($"case U.MenuDomain.Charlie: L.Log(\"Charlie-Add\", SessionID, 1); A.CharlieHelper?.Add(simChoice, simInputValues); break;", Context); // Add
            Assert.Contains($"case U.MenuDomain.Brent: L.Log(\"Brent-Add\", SessionID, 1); A.BrentHelper?.Add(simChoice, simInputValues); break;", Context); // Add

            Assert.Contains("{B03F74F5-9862-4916-9EF1-82DD253A5BC3}", Context); // View
            Assert.Contains("case U.MenuDomain.Charlie: L.Log(\"Charlie-View\", SessionID, 1);", Context); // View
            Assert.Contains("if (A.CharlieHelper != null && A.CharlieHelper.Items != null) { A.CharlieHelper?.Refresh(A.CharlieHelper.Items); }", Context); // View

            Assert.Contains("{ADECB8B3-1779-4107-9DF5-9E250E31AFDD}", Context); // First
            var Entity = "Charlie";
            Assert.Contains($"{{ADECB8B3-1779-4107-9DF5-9E250E31AFDD}}", Context); // First
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // First
            Assert.Contains($"L.Log(\"{Entity}- FirstPage\", SessionID, 1);", Context); // First
            Assert.Contains($"A.{Entity}Helper?.View(U.Navigation.FirstPage);", Context); // First
            Entity = "Brent";
            Assert.Contains($"{{ADECB8B3-1779-4107-9DF5-9E250E31AFDD}}", Context); // First
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // First
            Assert.Contains($"L.Log(\"{Entity}- FirstPage\", SessionID, 1);", Context); // First
            Assert.Contains($"A.{Entity}Helper?.View(U.Navigation.FirstPage);", Context); // First

            Entity = "Charlie";
            Assert.Contains("{39C53717-4163-4B33-B652-4AA3B4D28C5B}", Context); // Last
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Last
            Assert.Contains($"L.Log(\"{Entity}- LastPage\", SessionID, 1);", Context); // Last
            Assert.Contains($"A.{Entity}Helper?.View(U.Navigation.LastPage);", Context); // Last

            Entity = "Brent";
            Assert.Contains("{39C53717-4163-4B33-B652-4AA3B4D28C5B}", Context); // Last
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Last
            Assert.Contains($"L.Log(\"{Entity}- LastPage\", SessionID, 1);", Context); // Last
            Assert.Contains($"A.{Entity}Helper?.View(U.Navigation.LastPage);", Context); // Last

            Entity = "Charlie";
            Assert.Contains("{BBDE47EF-8937-4545-A019-652A8A306B6E}", Context); // Next
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Next
            Assert.Contains($"{Entity}-NextPage", Context); // Next
            Assert.Contains($"A.{Entity}Helper?.View(U.Navigation.NextPage);", Context); // Next

            Entity = "Brent";
            Assert.Contains("{BBDE47EF-8937-4545-A019-652A8A306B6E}", Context); // Next
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Next
            Assert.Contains($"{Entity}-NextPage", Context); // Next
            Assert.Contains($"A.{Entity}Helper?.View(U.Navigation.NextPage);", Context); // Next

            Entity = "Charlie";
            Assert.Contains("{90D2D480-6963-441E-B2C4-E0EADC878A83}", Context); // Previous
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Previous
            Assert.Contains($"L.Log(\"{Entity}-PreviousPage\", SessionID, 1);", Context); // Previous
            Assert.Contains($"A.{Entity}Helper?.View(U.Navigation.PreviousPage);", Context); // Previous

            Entity = "Brent";
            Assert.Contains("{90D2D480-6963-441E-B2C4-E0EADC878A83}", Context); // Previous
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Previous
            Assert.Contains($"L.Log(\"{Entity}-PreviousPage\", SessionID, 1);", Context); // Previous
            Assert.Contains($"A.{Entity}Helper?.View(U.Navigation.PreviousPage);", Context); // Previous

            Entity = "Charlie";
            Assert.Contains("{74A75AB1-1AB2-46C7-B63F-39F52AF0049A}", Context); // Previous
            Assert.Contains($"L.Log(\"{Entity}-Edit\", SessionID, 1);", Context);
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Previous
            Assert.Contains($"A.{Entity}Helper?.Edit(simChoice, simInputValues);", Context); // Previous

            Entity = "Brent";
            Assert.Contains("{74A75AB1-1AB2-46C7-B63F-39F52AF0049A}", Context); // Previous
            Assert.Contains($"L.Log(\"{Entity}-Edit\", SessionID, 1);", Context);
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Previous
            Assert.Contains($"A.{Entity}Helper?.Edit(simChoice, simInputValues);", Context); // Previous

            Entity = "Charlie";
            Assert.Contains("{11002DF2-E6AB-485E-B896-C3ED92706E30}", Context); // Remove
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Remove
            Assert.Contains($"L.Log(\"{Entity}-Remove\", SessionID, 1);", Context); // Remove
            Assert.Contains($"A.{Entity}Helper?.Remove(simChoice, simInputValues);", Context); // Remove

            Entity = "Brent";
            Assert.Contains("{11002DF2-E6AB-485E-B896-C3ED92706E30}", Context); // Remove
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Remove
            Assert.Contains($"L.Log(\"{Entity}-Remove\", SessionID, 1);", Context); // Remove
            Assert.Contains($"A.{Entity}Helper?.Remove(simChoice, simInputValues);", Context); // Remove

            Entity = "Charlie";
            Assert.Contains("{36DE75D7-A730-4F6B-A7C9-4660245BD895}", Context); // Save
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Save
            Assert.Contains($"L.Log(\"{Entity}-Save\", SessionID, 1);", Context); // Save
            Assert.Contains($"A.{Entity}Helper?.Save();", Context); // Save

            Entity = "Brent";
            Assert.Contains("{36DE75D7-A730-4F6B-A7C9-4660245BD895}", Context); // Save
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Save
            Assert.Contains($"L.Log(\"{Entity}-Save\", SessionID, 1);", Context); // Save
            Assert.Contains($"A.{Entity}Helper?.Save();", Context); // Save

            Entity = "Charlie";
            Assert.Contains("{36DE75D7-A730-4F6B-A7C9-4660245BD895}", Context); // Load
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Load
            Assert.Contains($"L.Log(\"{Entity}-Load\", SessionID, 1);", Context); // Load
            Assert.Contains($"A.{Entity}Helper?.Load();", Context); // Load

            Entity = "Charlie";
            Assert.Contains("{F745E72F-A908-4AB1-AF8B-E3FDE13BF46E}", Context); // Search
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Search
            Assert.Contains($"L.Log(\"{Entity}-Search\", SessionID, 1);", Context); // Search
            Assert.Contains($"A.{Entity}Helper?.Load();", Context); // Search

            Entity = "Charlie";
            Assert.Contains("{B2FED166-7FCF-4163-8507-EB1CC28B6435}", Context); // Refresh
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Refresh
            Assert.Contains($"case U.MenuDomain.{Entity}: L.Log(\"{Entity}-View\", SessionID, 1);", Context); // Refresh
            Assert.Contains($"if (A.{Entity}Helper != null && A.{Entity}Helper.Items != null && A.{Entity}Helper.Items.Count > 0)", Context); // Refresh
            Assert.Contains($"{{A.{Entity}Helper.Refresh(A.{Entity}Helper.Items);}}", Context); // Refresh

            Entity = "Charlie";
            Assert.Contains("{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}", Context); // Hidden
            Assert.Contains($"case U.MenuDomain.{Entity}:", Context); // Hidden
            Assert.Contains($"\tif (A.{Entity}Helper != null && ApplyHiddenRules(A.{Entity}Helper.Page) && doReIndex)", Context); // Hidden
            Assert.Contains($"\t{{A.{Entity}Helper.ReIndexDisplayId();}}", Context); // Hidden

            Assert.Contains("Brent", Context); //Find the GUID marker.           
            Assert.Contains("Charlie", Context); //Find the entry point

            RemoveCreatedModelFiles("Brent");
            RemoveCreatedModelFiles("Charlie");
            RestoreOriginalModels();
        }

        private static void RemoveCreatedModelFiles(string Entity)
        {
            var CSFile = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}.cs";
            var CSFileHelper = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}Helper.cs";
            var GeneratedFolder = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model";

            if (File.Exists(CSFile))
                File.Delete(CSFile);
            if (File.Exists(CSFileHelper))
                File.Delete(CSFileHelper);
            if (Directory.Exists(GeneratedFolder))
                Directory.Delete(GeneratedFolder);

        }

        private static void TestGenCode(bool GetBackup, string Entity)
        {
            CodeGen.SessionID = Guid.NewGuid().ToString();
            var CSFile = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}.cs";
            var CSFileHelper = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}Helper.cs";
            var GeneratedFolder = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model";
            var GeneratedModelsFolder = $"{CodeGen.WorkingFolder}\\GeneratedModels";

            var UniversalUtilitiesFile = $"{CodeGen.WorkingFolder}\\Utilities\\UniversalUtilities.cs";
            var UniversalUtilitiesFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Utilities\\UniversalUtilities.cs";

            var ArchLoaderFile = $"{CodeGen.WorkingFolder}\\Models\\ArchLoader.cs";
            var ArchLoaderFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Models\\ArchLoader.cs";

            var TargetTaskHelperFile = $"{CodeGen.WorkingFolder}\\Models\\TargetTaskHelper.cs";
            var TargetTaskHelperFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Models\\TargetTaskHelper.cs";

            if (GetBackup)
            {
                if (File.Exists(CSFile))
                    File.Delete(CSFile);

                if (File.Exists(CSFileHelper))
                    File.Delete(CSFileHelper);

                if (Directory.Exists(GeneratedFolder))
                    Directory.Delete(GeneratedFolder);

                if (File.Exists(ArchLoaderFile))
                    File.Delete(ArchLoaderFile);

                if (File.Exists(UniversalUtilitiesFile))
                    File.Delete(UniversalUtilitiesFile);                
                
                if (File.Exists(TargetTaskHelperFile))
                    File.Delete(TargetTaskHelperFile);

                if (File.Exists(UniversalUtilitiesFileBackup))
                    File.Copy(UniversalUtilitiesFileBackup, UniversalUtilitiesFile);

                if (File.Exists(ArchLoaderFileBackup))
                    File.Copy(ArchLoaderFileBackup, ArchLoaderFile);

                if (File.Exists(TargetTaskHelperFileBackup))
                    File.Copy(TargetTaskHelperFileBackup, TargetTaskHelperFile);

                Assert.True(File.Exists(UniversalUtilitiesFile));
                Assert.True(File.Exists(ArchLoaderFile));

            }

            //Checks
            var Context = File.ReadAllText(UniversalUtilitiesFile);
            Assert.Contains("//{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", Context); //Find the GUID marker.           
            Assert.Contains("//{E401C6FC-99B7-41B0-A612-8DABFE8734C3}", Context); //Find the entry point
            Context = File.ReadAllText(ArchLoaderFile);
            Assert.Contains("//{0ACDC688-3120-452F-94AE-2DD1771A9991}", Context); //Using GUID marker
            Assert.Contains("//{048A4DD6-2F1B-4178-A732-E3B50D3F0791}", Context); //public variable Helper GUID marker
            Assert.Contains("//{9ED7AF33-DE0E-45C3-821F-4669558AD744}", Context); //Instantiation of the Helper variable -  GUID marker

            //            
            bool Results = CodeGen.CreateDefaultCode(Entity);
            Assert.True(Results);

            //Assert
            Assert.True(Directory.Exists(GeneratedModelsFolder));
            Assert.True(Directory.Exists(GeneratedFolder));
            Assert.True(File.Exists(CSFile));
            Assert.True(File.Exists(CSFileHelper));
            Assert.True(File.Exists(UniversalUtilitiesFile));
            Assert.True(File.Exists(ArchLoaderFile));

            Context = File.ReadAllText(UniversalUtilitiesFile);
            Assert.Contains(Entity, Context);
            Assert.Contains("//{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", Context); //Find the GUID marker
            Assert.Contains("//{E401C6FC-99B7-41B0-A612-8DABFE8734C3}", Context); //Find the entry point GUID

            Context = File.ReadAllText(ArchLoaderFile);
            Assert.Contains("//{0ACDC688-3120-452F-94AE-2DD1771A9991}", Context); //Find the GUID marker
            Assert.Contains("", Context); //Find the GUID marker
            Assert.Contains($"using {Entity}Helper = ArchCorpUtilities.GeneratedModels.{Entity}Model.{Entity}Helper;", Context);
            Assert.Contains($"using ArchCorpUtilities.Models.{Entity}Model.{Entity};", Context);
        }

        [Fact]
        public void ValidTestGenCodeCreateMultipleEntities()
        {
            //CodeGen.SessionID = Guid.NewGuid().ToString();
            //var StartingGUID = CodeGen.SessionID;
            //var Entity = "Alpha";
            //var CSFile = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}.cs";
            //var CSFileHelper = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}Helper.cs";
            //var GeneratedFolder = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model";
            //var GeneratedModelsFolder = $"{CodeGen.WorkingFolder}\\GeneratedModels";
            //var UniversalUtilitiesFile = $"{CodeGen.WorkingFolder}\\Utilities\\UniversalUtilities.cs";
            //var UniversalUtilitiesFileBackup = $"{CodeGen.WorkingFolder}\\Utilities\\UniversalUtilities.{CodeGen.SessionID}";

            ////Checks
            //var Context = File.ReadAllText(UniversalUtilitiesFile);
            //Assert.Contains("//{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", Context); //Find the GUID marker.           
            //Assert.Contains("//{E401C6FC-99B7-41B0-A612-8DABFE8734C3}", Context); //Find the entry point
            //Context = "";
            ////            
            //bool Result = CodeGen.CreateDefaultCode(Entity);
            //Assert.True(Result);
            //Assert.True(Directory.Exists(GeneratedModelsFolder));
            //Assert.True(Directory.Exists(GeneratedFolder));
            //Assert.True(File.Exists(CSFile));
            //Assert.True(File.Exists(CSFileHelper));
            //Assert.True(File.Exists(UniversalUtilitiesFile));

            //Context = File.ReadAllText(UniversalUtilitiesFile);
            //Assert.Contains(Entity, Context);

            //Assert.Contains("//{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", Context); //Find the GUID marker
            //Assert.Contains("//{E401C6FC-99B7-41B0-A612-8DABFE8734C3}", Context); //Find the entry point GUID

            //Entity = "Beta";
            //Result = CodeGen.CreateDefaultCode(Entity);
            //Assert.True(Result);
            //var GUID = CodeGen.SessionID;
            //Assert.True(StartingGUID == GUID);
            //CodeGen.CreateDefaultCode(Entity);            
            //Assert.True(Directory.Exists(GeneratedModelsFolder));
            //Assert.True(Directory.Exists(GeneratedFolder));
            //Assert.True(File.Exists(CSFile));
            //Assert.True(File.Exists(CSFileHelper));
            //Assert.True(File.Exists(UniversalUtilitiesFile));

            //Context = File.ReadAllText(UniversalUtilitiesFile);
            //Assert.Contains(Entity, Context);

            //Assert.Contains("//{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", Context); //Find the GUID marker
            //Assert.Contains("//{E401C6FC-99B7-41B0-A612-8DABFE8734C3}", Context); //Find the entry point GUID

        }
    }
}