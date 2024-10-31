namespace TestProjectMenus
{
    using MBR = ArchCorpUtilities.Models.MockBuildingsRepository;
    using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
    public class UnitTestMenus
    {
        [Fact]
        public void ValidTestMapMenus()
        {
            var result = MH.ViewMenuStructure();
            Assert.NotNull(result);
        }
    }
}