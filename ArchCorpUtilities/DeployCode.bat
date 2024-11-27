Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Models\ C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities\Models\ ArchLoader.cs TargetTaskHelper.cs
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\EnumLib\ EnumLib.cs
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\GeneratedModels C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities\GeneratedModels  /S
cd C:\\_FLAP03\\GBZZBEBJ\\Working\\dotnet\\galactic-manager\\
echo Cleaning the project
Dotnet clean
echo Building the project
Dotnet build
Dotnet build -c Release
rem echo Copy EnumLib.dll to menuGen
rem del C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\GenMenu\bin\Debug\net8.0\EnumLib.dll /Q /S
rem del C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\GenMenu\bin\Debug\net8.0\EnumLib.pdb /Q /S
rem Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\GenCode\bin\Debug\net8.0 C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\GenMenu\bin\Debug\net8.0 EnumLib.dll EnumLib.pdb