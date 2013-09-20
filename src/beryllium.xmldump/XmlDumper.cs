using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using beryllium.lib;
using beryllium.lib.Model;
using beryllium.xmldump.Properties;



namespace beryllium.xmldump
{
   public class XmlDumper : IWorldProcessor {
      private static readonly XName AttribName_name = XName.Get("name");
      private static readonly XName AttribName_dirName = XName.Get("dirName");
      private static readonly XName AttribName_hasRegions = XName.Get("hasRegions");

      private static readonly XName NodeName_directoryInfo = XName.Get("directoryInfo");
      private static readonly XName NodeName_directory = XName.Get("directory");
      private static readonly XName NodeName_file = XName.Get("file");
      private static readonly XName NodeName_fullPath = XName.Get("fullPath");
      private static readonly XName NodeName_level = XName.Get("level");
      private static readonly XName NodeName_dimension = XName.Get("dimension");
      private static readonly XName NodeName_region = XName.Get("region");
      private static readonly XName NodeName_version = XName.Get("version");
      private static readonly XName NodeName_isInitialized = XName.Get("isInitialized");
      private static readonly XName NodeName_generatorName = XName.Get("generatorName");
      private static readonly XName NodeName_generatorVersion = XName.Get("generatorVersion");
      private static readonly XName NodeName_generatorOptions = XName.Get("generatorOptions");
      private static readonly XName NodeName_randomSeed = XName.Get("randomSeed");
      private static readonly XName NodeName_mapFeatures = XName.Get("mapFeatures");
      private static readonly XName NodeName_lastPlayed = XName.Get("lastPlayed");
      private static readonly XName NodeName_allowCommands = XName.Get("allowCommands");
      private static readonly XName NodeName_isHardCore = XName.Get("isHardCore");
      private static readonly XName NodeName_gameType = XName.Get("gameType");
      private static readonly XName NodeName_time = XName.Get("time");
      private static readonly XName NodeName_dayTime = XName.Get("dayTime");
      private static readonly XName NodeName_spawnX = XName.Get("spawnX");
      private static readonly XName NodeName_spawnY = XName.Get("spawnY");
      private static readonly XName NodeName_spawnZ = XName.Get("spawnZ");
      private static readonly XName NodeName_isRaining = XName.Get("isRaining");
      private static readonly XName NodeName_rainTime = XName.Get("rainTime");
      private static readonly XName NodeName_isThundering = XName.Get("isThundering");
      private static readonly XName NodeName_thunderTime = XName.Get("thunderTime");
      private static readonly XName NodeName_regionX = XName.Get("regionX");
      private static readonly XName NodeName_regionZ = XName.Get("regionZ");


      // TODO: use XmlWriter or something similar for forward-only writing so entire XML structure isn't maintained in memory
      //private readonly TextWriter _writer;

      private readonly XDocument _xdoc;

      private XElement _levelElem,
                       _dimElem;


      public XmlDumper() {
         _xdoc = XDocument.Parse(Resources.Blank_world);
      }


      public string GetXml() {
         return _xdoc.ToString();
      }


      public void ProcessLevelDirectory(LevelDirectoryMetadata levelDirectoryMetadata) {
         XElement dirInfoElem = new XElement(NodeName_directoryInfo);
         dirInfoElem.Add(new XElement(NodeName_fullPath, levelDirectoryMetadata.LevelDirectoryPath));

         XElement rootDirElem = getNodeElem(levelDirectoryMetadata.FileSystemDirectory, null);
         dirInfoElem.Add(rootDirElem);

         _xdoc.Root.Add(dirInfoElem);
      }


      private XElement getNodeElem(LevelDirectoryNode node, string name) {
         XElement nodeElem = new XElement(node.IsDirectory ? NodeName_directory : NodeName_file);
         if ( name != null )
            nodeElem.Add(new XAttribute(AttribName_name, name));

         foreach ( LevelDirectoryNode child in node.Children ) {
            XElement childElem = getNodeElem(child, child.Name);
            nodeElem.Add(childElem);
         }

         return nodeElem;
      }


      public void ProcessLevelMetadata(LevelMetadata levelMetadata) {
         _levelElem = new XElement(NodeName_level);
         _levelElem.Add(new XAttribute(AttribName_name, levelMetadata.LevelName));

         _levelElem.Add(new XElement(NodeName_version, levelMetadata.Version));
         _levelElem.Add(new XElement(NodeName_isInitialized, levelMetadata.IsInitialized));
         _levelElem.Add(new XElement(NodeName_generatorName, levelMetadata.GeneratorName));
         _levelElem.Add(new XElement(NodeName_generatorVersion, levelMetadata.GeneratorVersion));
         _levelElem.Add(new XElement(NodeName_generatorOptions, levelMetadata.GeneratorOptions));
         _levelElem.Add(new XElement(NodeName_randomSeed, levelMetadata.RandomSeed));
         _levelElem.Add(new XElement(NodeName_mapFeatures, levelMetadata.MapFeatures));
         _levelElem.Add(new XElement(NodeName_lastPlayed, levelMetadata.LastPlayed));
         _levelElem.Add(new XElement(NodeName_allowCommands, levelMetadata.AllowCommands));
         _levelElem.Add(new XElement(NodeName_isHardCore, levelMetadata.IsHardCore));
         _levelElem.Add(new XElement(NodeName_gameType, levelMetadata.GameType));
         _levelElem.Add(new XElement(NodeName_time, levelMetadata.Time));
         _levelElem.Add(new XElement(NodeName_dayTime, levelMetadata.DayTime));
         _levelElem.Add(new XElement(NodeName_spawnX, levelMetadata.SpawnX));
         _levelElem.Add(new XElement(NodeName_spawnY, levelMetadata.SpawnY));
         _levelElem.Add(new XElement(NodeName_spawnZ, levelMetadata.SpawnZ));
         _levelElem.Add(new XElement(NodeName_isRaining, levelMetadata.IsRaining));
         _levelElem.Add(new XElement(NodeName_rainTime, levelMetadata.RainTime));
         _levelElem.Add(new XElement(NodeName_isThundering, levelMetadata.IsThundering));
         _levelElem.Add(new XElement(NodeName_thunderTime, levelMetadata.ThunderTime));

         _xdoc.Root.Add(_levelElem);
      }


      public void ProcessDimensionMetadata(DimensionMetadata dimension) {
         _dimElem = new XElement(NodeName_dimension);
         _dimElem.Add(new XAttribute(AttribName_name, dimension.DimensionPointer.DimensionName));
         _dimElem.Add(new XAttribute(AttribName_dirName, dimension.DimensionPointer.DimensionDirectoryNode.Name));
         _dimElem.Add(new XAttribute(AttribName_hasRegions, dimension.HasRegions));
         _levelElem.Add(_dimElem);
      }


      public void ProcessRegionHeader(Region region) {
         XElement regionElem = new XElement(NodeName_region);
         regionElem.Add(new XAttribute(AttribName_name, region.RegionPointer.FileName));

         regionElem.Add(new XElement(NodeName_regionX, region.RegionPointer.RegionX));
         regionElem.Add(new XElement(NodeName_regionZ, region.RegionPointer.RegionZ));
         _dimElem.Add(regionElem);
      }


      public void ProcessChunkHeader(ChunkHeader chunkHeader) {
         // TODO
      }


      public void ProcessChunkData(Chunk chunk) {
         // TODO
      }


      public void ProcessRegionEnd(Region region) {
         // TODO
      }


      public void ProcessDimensionEnd(DimensionMetadata dimension) {
         // TODO
      }


      public void ProcessLevelEnd(LevelMetadata levelMetadata) {
         // TODO
      }
   }
}
