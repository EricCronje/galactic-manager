REM Copy ArchLoader.cs, TargetTaskHelper.cs from backup to the actual working folder
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Backup\{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}\Models\ C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities\Models\ ArchLoader.cs TargetTaskHelper.cs
REM Copy EnumLib.cs from backup to the actual working folder
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Backup\{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}\ C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\EnumLib\ EnumLib.cs


REM Copy EnumLib.cs from backup to CodeGen working folder
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Backup\{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}\ C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\ EnumLib.cs

REM Clean the actual working folder for the code.
rmdir C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities\GeneratedModels /S /Q
rmdir C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\GeneratedModels /S /Q
rmdir C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Models /S /Q
rmdir C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Utilities /S /Q
REM Copy to working folder CodeGen from backup GUID
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Backup\{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}\GeneratedModels C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\GeneratedModels /S
REM Copy to actual code working folder from backup.
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Backup\{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}\GeneratedModels C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities\GeneratedModels /S
REM Copy ArchLoader.cs, TargetTaskHelper.cs from generated to the actual working folder.
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Backup\{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}\Models\ C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Models\ ArchLoader.cs TargetTaskHelper.cs
REM Copy EnumLib.cs from generated to the actual working folder.
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Backup\{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}\ C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\EnumLib\ EnumLib.cs