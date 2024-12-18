del C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorporationDemo\bin\Debug\net8.0\{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708} /S/Q
del C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorporationDemo\bin\Release\net8.0\{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708} /S/Q
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Generate C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorporationDemo\bin\Debug\net8.0 "{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}_Menus"
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Generate C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorporationDemo\bin\Release\net8.0 "{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}_Menus"
Robocopy C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen\Generate C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\Ad_hoc_Tests\bin\Debug\net8.0 "{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}_Menus"
cd C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorporationDemo\bin\Debug\net8.0
move {CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}_Menus {CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}
cd C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorporationDemo\bin\Release\net8.0
move {CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}_Menus {CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}
cd C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\Ad_hoc_Tests\bin\Debug\net8.0
move {CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}_Menus {CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}