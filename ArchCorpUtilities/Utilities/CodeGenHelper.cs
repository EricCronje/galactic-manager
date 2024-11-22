using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;
using static ArchCorpUtilities.Utilities.CodeGen.CodePart;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Utilities;

public static class CodeGenHelper
{
    public static string? SessionID { get; set; }
    public static string WorkingFolder { get; set; }
    public static string TargetWorkingFolder { get; set; }
    public static string Version { get; set; }
    public static string BackupFolder { get; set; }
    public static string CurrentGuid { get; set; }
    public static string ProdGuid { get; set; }
    public static string DevGuid { get; set; }
    public static string TestGuid { get; set; }

    static CodeGenHelper()
    {
        ProdGuid = "{744852ea-d309-4f87-bbd2-03fe76ba877b}";
        DevGuid = "{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}";
        TestGuid = "{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}";

        WorkingFolder = @"C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen";
        TargetWorkingFolder = @"C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities";
        Version = "23.11.25";
        CurrentGuid = DevGuid;
        BackupFolder = $"\\Backup\\{CurrentGuid}";
    }


    public static bool CreateDefaultCode(string entity, MenuTypeEnum menuType = MenuTypeEnum.Manage, string? lHLink = null, string? rHLink = null)
    {
        if (string.IsNullOrWhiteSpace(entity))
            return false;

        List<CodePart> codeVault = [];
        var Header = U.GetGeneratedCodeHeader();

        if (menuType == MenuTypeEnum.Manage || menuType == MenuTypeEnum.Link)
        {
            CodePartMenuEnum codePartMenuEnum = new("\\Utilities", "UniversalUtilities.cs", entity, "{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", WorkingFolder, Header, "\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartMenuEnum);

            CodePartGenUsing codePart = new("\\Models", "ArchLoader.cs", entity, "{0ACDC688-3120-452F-94AE-2DD1771A9991}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePart);

            CodePartHelperInstance codePartHelperInstance = new("\\Models", "ArchLoader.cs", entity, "{048A4DD6-2F1B-4178-A732-E3B50D3F0791}", WorkingFolder, "", "\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartHelperInstance);

            CodePartISetInConstructorArch codePartISetInConstructorArch = new("\\Models", "ArchLoader.cs", entity, "{9ED7AF33-DE0E-45C3-821F-4669558AD744}", WorkingFolder, "", "\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartISetInConstructorArch);

            CodePartTargetTaskHelperUsing codePartTargetTaskHelperUsing = new("\\Models", "TargetTaskHelper.cs", entity, "{24D86755-6962-4074-BD9F-73E8FE0A5F68}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePartTargetTaskHelperUsing);

            CodePartNodal codePartNodal = new("\\Models", "TargetTaskHelper.cs", entity, "{2D8B5F74-6CC5-4C0F-AB99-8E596C463DA0}", WorkingFolder, "", "\t\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartNodal);

            CodePartArchLoaderUsing codePartArchLoaderUsing = new("\\Models", "TargetTaskHelper.cs", entity, "{EA6AE6CA-7E51-43DE-95F9-FF66E27AE130}", WorkingFolder, "", "", SessionID ?? "TBA");
            codeVault.Add(codePartArchLoaderUsing);

            CodePartAdd codePartAdd = new("\\Models", "TargetTaskHelper.cs", entity, "{A6E3C6F1-D649-45CE-8C05-3A87466618A9}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartAdd);

            CodePartView codePartView = new("\\Models", "TargetTaskHelper.cs", entity, "{B03F74F5-9862-4916-9EF1-82DD253A5BC3}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartView);

            CodePartFirst codePartFirst = new("\\Models", "TargetTaskHelper.cs", entity, "{ADECB8B3-1779-4107-9DF5-9E250E31AFDD}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartFirst);

            CodePartLast codePartLast = new("\\Models", "TargetTaskHelper.cs", entity, "{39C53717-4163-4B33-B652-4AA3B4D28C5B}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartLast);

            CodePartNext codePartNext = new("\\Models", "TargetTaskHelper.cs", entity, "{BBDE47EF-8937-4545-A019-652A8A306B6E}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartNext);

            CodePartPrevious codePartPrevious = new("\\Models", "TargetTaskHelper.cs", entity, "{90D2D480-6963-441E-B2C4-E0EADC878A83}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartPrevious);

            CodePartEdit codePartEdit = new("\\Models", "TargetTaskHelper.cs", entity, "{74A75AB1-1AB2-46C7-B63F-39F52AF0049A}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartEdit);

            CodePartRemove codePartRemove = new("\\Models", "TargetTaskHelper.cs", entity, "{11002DF2-E6AB-485E-B896-C3ED92706E30}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartRemove);

            CodePartSave codePartSave = new("\\Models", "TargetTaskHelper.cs", entity, "{36DE75D7-A730-4F6B-A7C9-4660245BD895}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartSave);

            CodePartLoad codePartLoad = new("\\Models", "TargetTaskHelper.cs", entity, "{BD92B12F-6AB8-420C-9A4B-654233721FB7}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartLoad);

            CodePartSearch codePartSearch = new("\\Models", "TargetTaskHelper.cs", entity, "{F745E72F-A908-4AB1-AF8B-E3FDE13BF46E}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartSearch);

            CodePartRefresh codePartRefresh = new("\\Models", "TargetTaskHelper.cs", entity, "{B2FED166-7FCF-4163-8507-EB1CC28B6435}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartRefresh);

            CodePartHiddenRules codePartHiddenRules = new("\\Models", "TargetTaskHelper.cs", entity, "{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}", WorkingFolder, "", "\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartHiddenRules);

            CodePartLoadDefaults codePartLoadDefaults = new("\\Models", "ArchLoader.cs", entity, "{E4C217C0-AC0D-4571-95E4-16CE056F35A5}", WorkingFolder, "", "\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartLoadDefaults);

            CodePartCreatePogo codePartCreatePogo = new("\\GeneratedModels", $"{entity}.cs", entity, "{2F1F31FC-636B-4FA1-B1F5-BD767B125F0E}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePartCreatePogo);

            CodePartCreateHelper codePartCreateHelper = new("\\GeneratedModels", $"{entity}Helper.cs", entity, "{20D3B776-48B9-43E0-AE40-F1ABBCC31B90}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePartCreateHelper);

            CodePartCreateMockRepository codePartCreatMockReposirory = new("\\GeneratedModels", $"{entity}MockRepository.cs", entity, "{0A673A7C-C929-442E-87EE-077C5267B9C3}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePartCreatMockReposirory);
        }

        if (menuType == MenuTypeEnum.Link)
        {
            CodePartCreatePogoLink codePartCreatePogoLink = new("\\GeneratedModels", $"{entity}.cs", entity, "{BC048EB7-5741-4D41-8608-208AEFDE31E1}", WorkingFolder, Header, "", SessionID ?? "TBA", lHLink, rHLink);
            codeVault.Add(codePartCreatePogoLink);

            CodePartCreateHelperLink codePartCreateHelperLink = new("\\GeneratedModels", $"{entity}Helper.cs", entity, "{40CEF4E7-3F18-41F1-8149-01DF4FFFF9D9}", WorkingFolder, Header, "", SessionID ?? "TBA", lHLink, rHLink);
            codeVault.Add(codePartCreateHelperLink);
        }

        foreach (CodePart item in codeVault)
            if (!item.AlterCode())
                return false;

        return true;
    }




}
