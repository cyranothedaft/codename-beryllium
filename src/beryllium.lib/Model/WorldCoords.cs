using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public enum WorldCoordUnit {
      Block,
      Chunk,
      Region,
   }


   public class WorldCoords {
      internal const double RegionToBlockFactor = 32 * 16;
      internal const double ChunkToBlockFactor = 16;

      public WorldCoordUnit Units { get; private set; }
      public int X { get; private set; }
      public int Z { get; private set; }

      public WorldCoords(WorldCoordUnit units, int x, int z) {
         Units = units;
         X = x;
         Z = z;
      }

      // copy constructor
      //public WorldCoords(WorldCoords src)
      //   : this(src.Units, src.X, src.Z) {
      //}


      public WorldCoords ConvertTo(WorldCoordUnit toUnits) {
         double factor = getConversionFactor(this.Units, toUnits);
         return new WorldCoords(toUnits, ( int )( this.X * factor ), ( int )( this.Z * factor ));
      }


      private static double getConversionFactor(WorldCoordUnit fromUnits, WorldCoordUnit toUnits) {
         switch ( toUnits ) {
            case WorldCoordUnit.Block:
               switch ( fromUnits ) {
                  case WorldCoordUnit.Chunk:   return ChunkToBlockFactor;
                  case WorldCoordUnit.Region:  return RegionToBlockFactor;
               }
         }
         throw new InvalidOperationException(string.Format("WorldCoord conversion not supported from {0} to {1}", fromUnits, toUnits));
      }
   }


   public sealed class RelativeWorldCoords : WorldCoords {
      public WorldCoords RelativeTo { get; private set; }

      public RelativeWorldCoords(WorldCoords relativeTo, int offsetX, int offsetZ)
         : base(relativeTo.Units, offsetX, offsetZ) {
         RelativeTo = relativeTo;
      }
   }



   //public abstract class WorldCoords {
   //   internal const int RegionToBlockFactor = 32 * 16;
   //   internal const int ChunkToBlockFactor = 16;

   //   public int X, Z;

   //   protected WorldCoords() { }

   //   protected WorldCoords(int x, int z) {
   //      X = x;
   //      Z = z;
   //   }
   //}


   //public sealed class WorldCoords_Region : WorldCoords {
   //   public WorldCoords_Region() { }

   //   public WorldCoords_Region(int x, int z) : base(x, z) { }

   //   public WorldCoords_Region Offset(int difX, int difZ) {
   //      return new WorldCoords_Region(X + difX, Z + difZ);
   //   }
   //}


   //public sealed class WorldCoords_Chunk : WorldCoords {
   //   public WorldCoords_Chunk() { }

   //   public WorldCoords_Chunk(int x, int z) : base(x, z) { }
   //}


   //public sealed class WorldCoords_Block : WorldCoords {
   //   public WorldCoords_Block() { }

   //   public WorldCoords_Block(int x, int z) : base(x, z) { }

   //   public WorldCoords_Block(WorldCoords_Chunk worldCoords_Chunk)
   //      : base(ChunkToBlockFactor * worldCoords_Chunk.X,
   //             ChunkToBlockFactor * worldCoords_Chunk.Z) {
   //   }


   //   public WorldCoords_Block Offset(int difX, int difZ) {
   //      return new WorldCoords_Block(X + difX, Z + difZ);
   //   }
   //}
}
