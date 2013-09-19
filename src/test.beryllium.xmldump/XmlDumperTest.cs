using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using beryllium.lib;
using beryllium.xmldump;
using test.beryllium.xmldump.Properties;



namespace test.beryllium.xmldump
{
   [TestFixture]
   public class XmlDumperTest {
      [Test]
      public void test1() {
         const string levelFilePath = @"C:\files\personal\proj\dev\codename-beryllium\data\SampleWorlds\flat test 1";
         WorldReader worldReader = new WorldReader();
         XmlDumper xmlDumper = new XmlDumper();
         worldReader.RegisterProcessor(xmlDumper);
         worldReader.Process(levelFilePath);

         string xml = xmlDumper.GetXml();
         Assert.AreEqual(Resources.XmlDumperTest_flatTest1_result, xml);
      }
   }
}
