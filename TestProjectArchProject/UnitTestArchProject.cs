using ArchCorpUtilities.Models.Buildings;
using ArchCorpUtilities.Models.Menus;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using System.Text;

using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace TestProjectArchProject
{
    public class UnitTestArchProject
    {

        [Fact]
        public void ValidCheckMenuItems()
        {
            Assert.False(MH.Menu == null);
            StringBuilder sb = new();
            if (MH.Menu != null)
            {
                sb.AppendJoin("|", MH.Menu.AsEnumerable<MenuItem>().Select(p => p.DisplayName));

                string result = sb.ToString();

                Assert.Contains("View Menus", result);
                Assert.Contains("Remove Menus", result);
                Assert.Contains("Add Menus", result);
                Assert.Contains("Edit Menus", result);
                Assert.Contains("Save menus to a file", result);
                Assert.Contains("Load menus from a file", result);
            }
        }

        [Fact]
        public void ValidExportMenu()
        {
            string Path = "TestMenus.txt";
            MH.ExportMenus(Path);
            Assert.True(File.Exists("TestMenus.txt"));
            Assert.Contains("|", File.ReadAllText(Path));
            File.Delete(Path);
        }

    }
}
