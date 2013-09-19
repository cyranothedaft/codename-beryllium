using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;



namespace beryllium.lib {
   internal sealed class MasterProcessor : IWorldProcessor {
      private readonly List<IWorldProcessor> _processors = new List<IWorldProcessor>();


      public void AddProcessor(IWorldProcessor processor) {
         _processors.Add(processor);
      }


      public void ProcessLevelMetadata(LevelMetadata levelMetadata) {
         foreach ( IWorldProcessor processor in _processors ) processor.ProcessLevelMetadata(levelMetadata);
      }

      public void ProcessDimensionMetadata(DimensionMetadata dimension) {
         foreach ( IWorldProcessor processor in _processors ) processor.ProcessDimensionMetadata(dimension);
      }

      public void ProcessRegionHeader(RegionHeader regionHeader) {
         foreach ( IWorldProcessor processor in _processors ) processor.ProcessRegionHeader(regionHeader);
      }

      public void ProcessChunkHeader(ChunkHeader chunkHeader) {
         foreach ( IWorldProcessor processor in _processors ) processor.ProcessChunkHeader(chunkHeader);
      }

      public void ProcessChunkData(Chunk chunk) {
         foreach ( IWorldProcessor processor in _processors ) processor.ProcessChunkData(chunk);
      }

      public void ProcessRegionEnd(Region region) {
         foreach ( IWorldProcessor processor in _processors ) processor.ProcessRegionEnd(region);
      }

      public void ProcessDimensionEnd(DimensionMetadata dimension) {
         foreach ( IWorldProcessor processor in _processors ) processor.ProcessDimensionEnd(dimension);
      }

      public void ProcessLevelEnd(LevelMetadata levelMetadata) {
         foreach ( IWorldProcessor processor in _processors ) processor.ProcessLevelEnd(levelMetadata);
      }
   }
}
