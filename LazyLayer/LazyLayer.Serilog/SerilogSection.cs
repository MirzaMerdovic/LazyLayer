using System.Configuration;

namespace LazyLayer.Serilog
{
    public class SerilogSection : ConfigurationSection
    {
        public SerilogSection()
        {
        }

        [ConfigurationProperty("MsSql")]
        public MsSqlSettings SqlServer
        {
            get { return (MsSqlSettings)this["MsSql"]; }
        }

        [ConfigurationProperty("RollingFile")]
        public RollingFileSettings RollingFile
        {
            get { return (RollingFileSettings)this["RollingFile"]; }
        }
    }

    public class MsSqlSettings : ConfigurationElement
    {
        private MsSqlSettings()
        {
        }

        [ConfigurationProperty("ConnectionString")]
        public string ConnectionString
        {
            get { return (string)this["ConnectionString"]; }
            set { this["ConnectionString"] = value; }
        }
        [ConfigurationProperty("TableName", DefaultValue = "Logs")]
        public string TableName
        {
            get { return (string)this["TableName"]; }
            set { this["TableName"] = value; }
        }
    }

    public class RollingFileSettings : ConfigurationElement
    {
        public RollingFileSettings()
        {
        }

        [ConfigurationProperty("LogPath")]
        public string LogPath
        {
            get { return (string)this["LogPath"]; }
            set { this["LogPath"] = value; }
        }
    }
}