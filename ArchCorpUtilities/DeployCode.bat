Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Models\ C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities\Models\ ArchLoader.cs TargetTaskHelper.cs
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\EnumLib\ EnumLib.cs
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\GeneratedModels C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities\GeneratedModels  /S
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Generate\ C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorporationDemo\bin\Debug\net8.0\ {CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}_Ready
cd C:\\_FLAP03\\GBZZBEBJ\\Working\\dotnet\\galactic-manager\\
echo Cleaning the project
Dotnet clean
echo Building the project
Dotnet build