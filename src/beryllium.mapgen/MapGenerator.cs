using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib;
using beryllium.lib.Model;



namespace beryllium.mapgen {
   public sealed class MapGenerator : IWorldProcessor {
      private readonly string _outputDir;


      public MapGenerator(string outputDir) {
         _outputDir = outputDir;
      }


      public void ProcessLevelDirectory(LevelDirectoryMetadata levelDirectoryMetadata) {
         //
      }


      public void ProcessLevelMetadata(LevelMetadata levelMetadata) {
         //
      }


      public void ProcessDimensionMetadata(DimensionMetadata dimension) {
         //
      }


      public void ProcessRegionHeader(Region region) {
         //
      }


      public void ProcessChunk(Chunk chunk) {
         //
      }


      public void ProcessRegionEnd(Region region) {
         //
      }


      public void ProcessDimensionEnd(DimensionMetadata dimension) {
         //
      }


      public void ProcessLevelEnd(LevelMetadata levelMetadata) {
         //
      }
   }
}
