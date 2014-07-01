using System.IO;

namespace SpecFlow.Reporting
{
    /// <summary>
    /// Base class for reporter implementations
    /// </summary>
    public abstract class Reporter
    {
        /// <summary>
        /// Feature currently running
        /// </summary>
        public Feature CurrentFeature { get; internal set; }

        /// <summary>
        /// Scenario currently running
        /// </summary>
        public Scenario CurrentScenario { get; internal set; }

        /// <summary>
        /// ScenarioBlock currently running
        /// </summary>
        public ScenarioBlock CurrentScenarioBlock { get; internal set; }

        /// <summary>
        /// Step currently running
        /// </summary>
        public Step CurrentStep { get; internal set; }

        /// <summary>
        /// Name of this reporter instance
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.GetType().FullName;
            }
        }

        /// <summary>
        /// The report currently being created
        /// </summary>
        public Report Report { get; internal set; }

        /// <summary>
        /// Serializes this reporter's report to the given <paramref name="path" />
        /// </summary>
        /// <param name="path"></param>
        public virtual void WriteToFile(string path)
        {
            using (var ms = new MemoryStream())
            {
                WriteToStream(ms);
                ms.Seek(0, SeekOrigin.Begin);
                using (var fs = File.Create(path))
                {
                    ms.CopyTo(fs);
                }
            }
        }

        /// <summary>
        /// Serializes this report's report to the given <paramref name="stream" />
        /// </summary>
        /// <param name="stream"></param>
        public abstract void WriteToStream(Stream stream);

        public string WriteToString()
        {
            using (var stream = new MemoryStream())
            {
                WriteToStream(stream);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}