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
         LevelDirectoryScanner levelDirectoryScanner = new LevelDirectoryScanner(levelFilePath);

         LevelDirectoryMetadata levelDirectoryMetadata = levelDirectoryScanner.Scan();
         _masterProc.ProcessLevelDirectory(levelDirectoryMetadata);

         LevelMetadata levelMetadata = LevelDatReader.ReadMetadata(levelDirectoryMetadata.LevelDatFilePath);
         _masterProc.ProcessLevelMetadata(levelMetadata);

         foreach ( DimensionPointer dimensionPointer in levelDirectoryMetadata.DimensionPointers ) {
            DimensionMetadata dimension = DimensionScanner.ReadMetadata(dimensionPointer);
            _masterProc.ProcessDimensionMetadata(dimension);

            if ( dimension.HasRegions ) {
               foreach ( RegionPointer regionPointer in dimension.RegionPointers ) {
                  RegionReader regionReader = new RegionReader(regionPointer);
                  Region region = regionReader.ReadHeader();
                  _masterProc.ProcessRegionHeader(region);

                  regionReader.ReadChunks(region, _masterProc.ProcessChunk);

                  _masterProc.ProcessRegionEnd(region);
               }
            }

            _masterProc.ProcessDimensionEnd(dimension);
         }

         _masterProc.ProcessLevelEnd(levelMetadata);
      }
   }
}
