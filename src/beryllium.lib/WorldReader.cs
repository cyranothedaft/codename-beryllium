using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Readers;



namespace beryllium.lib {
   public sealed class WorldReader {
      private readonly MasterProcessor _masterProc = new MasterProcessor();

      public void RegisterProcessor(IWorldProcessor processor) {
         // TODO
      }


      public void Read(string levelFilePath) {
         LevelMetadata levelMetadata = LevelReader.ReadMetadata(levelFilePath);
         _masterProc.ProcessLevelMetadata(levelMetadata);

         foreach ( DimensionPointer dimensionPointer in levelMetadata.DimensionPointers ) {
            DimensionMetadata dimension = DimensionScanner.ReadMetadata(dimensionPointer);
            _masterProc.ProcessDimensionMetadata(dimension);

            foreach ( RegionPointer regionPointer in dimension.RegionPointers ) {
               RegionReader regionReader = new RegionReader(regionPointer);
               Region region = regionReader.ReadHeader();
               _masterProc.ProcessRegionHeader(region.HeaderData);

               regionReader.ReadData(regionPointer, region);
               foreach ( ChunkPointer chunkPointer in region.ChunkPointers ) {
                  Chunk chunk = regionReader.ReadChunkHeader(chunkPointer);
                  _masterProc.ProcessChunkHeader(chunk.Header);

                  regionReader.ReadChunkData(chunkPointer, chunk);
                  _masterProc.ProcessChunkData(chunk);
               }

               _masterProc.ProcessRegionEnd(region);
            }

            _masterProc.ProcessDimensionEnd(dimension);
         }

         _masterProc.ProcessLevelEnd(levelMetadata);
      }
   }
}
