﻿using ArchCorpUtilities.Utilities.CodeGen;
using System.Text;
using static ArchCorpUtilities.Utilities.CodeGen.CodePart;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using E = EnumLib.EnumLib;
using M = MenuEnumLib.MenuEnumLib;

namespace ArchCorpUtilities.Utilities;

public static class CodeGenHelper
{
    public static string? SessionID { get; set; }
    public static string WorkingFolder { get; set; }
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

        Version = "23.11.25";
        CurrentGuid = DevGuid;
        BackupFolder = $"\\Backup\\{CurrentGuid}";
    }


    public static bool CreateDefaultCode(string entity, M.MenuTypeEnum menuType = M.MenuTypeEnum.Manage, string? lHLink = null, string? rHLink = null)
    {
        if (string.IsNullOrWhiteSpace(entity))
            return false;

        List<CodePart> codeVault = [];
        var Header = U.GetGeneratedCodeHeader();

        if (menuType == M.MenuTypeEnum.Hierarchy)
        {
            CodePartGenUsingHierarchy codePart = new("\\Models", "ArchLoader.cs", entity, "{5334A0BE-D696-4065-B673-A2113B7907A9}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePart);
            CodePartISetInConstructorArchHierarchy codePartISetInConstructorArch = new("\\Models", "ArchLoader.cs", entity, "{12C91723-1389-4BCC-866B-FB3E3C50D267}", WorkingFolder, "", "\t\t\t", SessionID ?? "TBA", lHLink);
            codeVault.Add(codePartISetInConstructorArch);
        }
        if (menuType == M.MenuTypeEnum.Link)
        {
            CodePartGenUsingLink codePart = new("\\Models", "ArchLoader.cs", entity, "{0ACDC688-3120-452F-94AE-2DD1771A9991}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePart);
            CodePartISetInConstructorArchLink codePartISetInConstructorArch = new("\\Models", "ArchLoader.cs", entity, "{298F4945-829B-4881-AF7C-9427FB5FCC59}", WorkingFolder, "", "\t\t\t", SessionID ?? "TBA", lHLink, rHLink);
            codeVault.Add(codePartISetInConstructorArch);
        }
        if (menuType == M.MenuTypeEnum.Manage)
        {
            CodePartGenUsingDefault codePart = new("\\Models", "ArchLoader.cs", entity, "{99B979B3-BA78-4173-959C-1F116C96BB04}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePart);
            CodePartISetInConstructorArch codePartISetInConstructorArch = new("\\Models", "ArchLoader.cs", entity, "{9ED7AF33-DE0E-45C3-821F-4669558AD744}", WorkingFolder, "", "\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartISetInConstructorArch);
        }

        if (menuType == M.MenuTypeEnum.Manage || menuType == M.MenuTypeEnum.Link || menuType == M.MenuTypeEnum.Hierarchy)
        {
            CodePartMenuEnum codePartMenuEnum = new("", "EnumLib.cs", entity, "{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", WorkingFolder, Header, "\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartMenuEnum);

            CodePartHelperInstance codePartHelperInstance = new("\\Models", "ArchLoader.cs", entity, "{048A4DD6-2F1B-4178-A732-E3B50D3F0791}", WorkingFolder, "", "\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartHelperInstance);

            CodePartArchShowMenu codePartArchShowMenu = new("\\Models", "ArchLoader.cs", entity, "{0EFC2DF7-9635-48A9-8A37-ED03992483F6}", WorkingFolder, "", "\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartArchShowMenu);

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

            CodePartClear codePartClear = new("\\Models", "TargetTaskHelper.cs", entity, "{F85FF648-A0B3-45FA-9784-8E3F1528B870}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartClear);

            CodePartHiddenRules codePartHiddenRules = new("\\Models", "TargetTaskHelper.cs", entity, "{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}", WorkingFolder, "", "\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartHiddenRules);

            CodePartLoadDefaults codePartLoadDefaults = new("\\Models", "ArchLoader.cs", entity, "{E4C217C0-AC0D-4571-95E4-16CE056F35A5}", WorkingFolder, "", "\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartLoadDefaults);
        }

        foreach (CodePart item in codeVault)
        if (!item.AlterCode())
            return false;

        return true;
    }




}
