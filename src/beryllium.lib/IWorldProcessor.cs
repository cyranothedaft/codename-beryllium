﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;



namespace beryllium.lib {
   public interface IWorldProcessor {
      void ProcessLevelMetadata(LevelMetadata levelMetadata);

      void ProcessDimensionMetadata(DimensionMetadata dimension);

      void ProcessRegionHeader(RegionHeader regionHeader);

      void ProcessChunkHeader(ChunkHeader chunkHeader);

      void ProcessChunkData(Chunk chunk);

      void ProcessRegionEnd(Region region);

      void ProcessDimensionEnd(DimensionMetadata dimension);

      void ProcessLevelEnd(LevelMetadata levelMetadata);
   }
}
