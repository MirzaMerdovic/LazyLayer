using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LazyLayer.Serilog.Test
{
    [TestClass]
    public class SerilogSettingsTests
    {
        private const string CategoryName = "SerilogSettings";

        [TestMethod, TestCategory(CategoryName)]
        public void ShouldReadConfigurationSection()
        {
            var section = ConfigurationManager.GetSection("SerilogSettings");
            SerilogSection serilog = section as SerilogSection;

            Assert.IsNotNull(serilog);
        }

        [TestMethod, TestCategory(CategoryName)]
        public void ShouldReadMsSqlConfigurationElement()
        {
            var section = (SerilogSection)ConfigurationManager.GetSection("SerilogSettings");

            Assert.IsNotNull(section.SqlServer);
            Assert.AreEqual("myConnectionString", section.SqlServer.ConnectionString);
            Assert.AreEqual("Logs", section.SqlServer.TableName);
        }

        [TestMethod, TestCategory(CategoryName)]
        public void ShouldReadRollingFileConfigurationElement()
        {
            var section = (SerilogSection)ConfigurationManager.GetSection("SerilogSettings");

            Assert.IsNotNull(section.RollingFile);
            Assert.AreEqual(".\\bin\\logs\\Lazy.log", section.RollingFile.LogPath);
        }
    }
}
