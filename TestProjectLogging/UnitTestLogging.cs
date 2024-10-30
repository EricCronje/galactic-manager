namespace TestProjectLogging
{
    public class UnitTestLogging
    {
        [Fact]
        public void ValidTestLog()
        {
            var Path = "logTest";
            if (File.Exists(Path)) 
            { 
                File.Delete(Path); 
            }
            Logger.Logger.Log("Test","",0, Path);
            Assert.True(File.Exists(Path));
            var lines = File.ReadAllLines(Path);
            var found = lines.FirstOrDefault(l => l.Contains("Test"));
            Assert.NotNull(found);         
            Assert.True(lines.Count() == 1);
            //Cleanup
            File.Delete(Path);
        }
    }
}