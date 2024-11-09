using MBR = ArchCorpUtilities.Models.Buildings.MockBuildingsRepository;
using P = Patina.Patina;
namespace TestProjectWithPagination
{
    public class UnitTestPagination
    {
        [Fact]
        public void ValidPagination()
        {
            //Get list of buildings
            MBR mockBuildingsRepository = new();
            UInt32 MaxItems = Convert.ToUInt32(mockBuildingsRepository?.All()?.ToList().Count);
            UInt32 PageSize = 2;
            P pagina = new(PageSize, MaxItems);
            pagina.GoToLastPage();
            var PaginizedBuildings = mockBuildingsRepository?.All()?.ToList().GetRange(Convert.ToInt32(pagina.GetFirstItemNumberOnPage0Based()), Convert.ToInt32(pagina.GetItemCountOnPage()));
            Assert.True(PaginizedBuildings?.Count == 1);
            Assert.Equal("Dragon One", PaginizedBuildings[0].Name);
            pagina.GetPreviousPage();
            PaginizedBuildings = mockBuildingsRepository?.All()?.ToList().GetRange(Convert.ToInt32(pagina.GetFirstItemNumberOnPage0Based()), Convert.ToInt32(pagina.GetItemCountOnPage()));
            Assert.True(PaginizedBuildings?.Count == 2);
            Assert.Equal("Alpha", PaginizedBuildings[0].Name);
            Assert.Equal("Beta", PaginizedBuildings[1].Name);
        }
    }
}