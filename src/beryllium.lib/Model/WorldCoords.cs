using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public abstract class WorldCoords {
      internal const int RegionToBlockFactor = 32 * 16;
      internal const int ChunkToBlockFactor = 16;

      public int X, Z;

      protected WorldCoords() { }

      protected WorldCoords(int x, int z) {
         X = x;
         Z = z;
      }
   }


   public sealed class WorldCoords_Region : WorldCoords {
      public WorldCoords_Region() { }

      public WorldCoords_Region(int x, int z) : base(x, z) { }

      public WorldCoords_Region Offset(int difX, int difZ) {
         return new WorldCoords_Region(X + difX, Z + difZ);
      }
   }


   public sealed class WorldCoords_Chunk : WorldCoords {
      public WorldCoords_Chunk() { }

      public WorldCoords_Chunk(int x, int z) : base(x, z) { }
   }


   public sealed class WorldCoords_Block : WorldCoords {
      public WorldCoords_Block() { }

      public WorldCoords_Block(int x, int z) : base(x, z) { }

      public WorldCoords_Block(WorldCoords_Chunk worldCoords_Chunk)
         : base(ChunkToBlockFactor * worldCoords_Chunk.X,
                ChunkToBlockFactor * worldCoords_Chunk.Z) {
      }


      public WorldCoords_Block Offset(int difX, int difZ) {
         return new WorldCoords_Block(X + difX, Z + difZ);
      }
   }
}
