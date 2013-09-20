using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using beryllium.lib;
using beryllium.stats;
using test.beryllium.xmldump.Properties;



namespace test.beryllium.xmldump {
   [TestFixture]
   public class StatsProcessorTest {
      [Test]
      public void test1() {
         string levelFilePath = Path.Combine(Settings.Default.TestDataPath_xeat, @"SampleWorlds\flat test 1");
         WorldReader worldReader = new WorldReader();
         StatsProcessor statsProcessor = new StatsProcessor();
         worldReader.RegisterProcessor(statsProcessor);
         worldReader.Process(levelFilePath);

         string csv = statsProcessor.GetCsv();
         Assert.AreEqual(Resources.StatsProcessorTest_flatTest1_result, csv);
      }
   }
}
