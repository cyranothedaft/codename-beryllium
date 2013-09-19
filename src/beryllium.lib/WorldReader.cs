﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;
using beryllium.lib.Readers;



namespace beryllium.lib {
   public sealed class WorldReader {
      private readonly MasterProcessor _masterProc = new MasterProcessor();

      public void RegisterProcessor(IWorldProcessor processor) {
         _masterProc.AddProcessor(processor);
      }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="levelFilePath">Refers either to a level.dat file or a world level directory that contains a level.dat file.</param>
      public void Process(string levelFilePath) {
         LevelMetadata levelMetadata = LevelReader.ReadMetadata(levelFilePath);
         _masterProc.ProcessLevelMetadata(levelMetadata);

//         foreach ( DimensionPointer dimensionPointer in levelMetadata.DimensionPointers ) {
//            DimensionMetadata dimension = DimensionScanner.ReadMetadata(dimensionPointer);
//            _masterProc.ProcessDimensionMetadata(dimension);
//
//            foreach ( RegionPointer regionPointer in dimension.RegionPointers ) {
//               RegionReader regionReader = new RegionReader(regionPointer);
//               Region region = regionReader.ReadHeaderOnly();
//               _masterProc.ProcessRegionHeader(region.HeaderData);
//
//               regionReader.ReadData(regionPointer, region);
//               foreach ( ChunkPointer chunkPointer in region.ChunkPointers ) {
//                  Chunk chunk = regionReader.ReadChunkHeaderOnly(chunkPointer);
//                  _masterProc.ProcessChunkHeader(chunk.Header);
//
//                  regionReader.ReadChunkData(chunkPointer, chunk);
//                  _masterProc.ProcessChunkData(chunk);
//               }
//
//               _masterProc.ProcessRegionEnd(region);
//            }
//
//            _masterProc.ProcessDimensionEnd(dimension);
//         }

         _masterProc.ProcessLevelEnd(levelMetadata);
      }
   }
}
