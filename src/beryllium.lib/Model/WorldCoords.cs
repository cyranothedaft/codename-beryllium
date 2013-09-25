using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public enum WorldCoordUnit {
      None,
      Block,
      Chunk,
      Region,
   }


   [DebuggerDisplay("{DebugString}")]
   public class WorldCoords {
      internal const double RegionToBlockFactor = 32 * 16;
      internal const double ChunkToBlockFactor = 16;

      private static readonly WorldCoords _zero = new WorldCoords();
      public static WorldCoords Zero { get { return _zero; } }

      public WorldCoordUnit Units { get; private set; }
      public int RawX { get; private set; }
      public int RawZ { get; private set; }
      public WorldCoords RelativeTo { get; private set; } // defaults to (0,0) (absolute)
      public int X { get; private set; }
      public int Z { get; private set; }


      // only for instantiating Zero instance
      private WorldCoords() {
         Units = WorldCoordUnit.None;
         X = RawX = 0;
         Z = RawZ = 0;
         RelativeTo = WorldCoords.Zero;
      }

      public WorldCoords(WorldCoordUnit units, int x, int z, WorldCoords relativeTo) {
         Units = units;
         RawX = x;
         RawZ = z;
         RelativeTo = relativeTo ?? WorldCoords.Zero;
         X = RelativeTo.X + x;
         Z = RelativeTo.Z + z;
      }

      public WorldCoords(WorldCoordUnit units, int x, int z)
         : this(units, x, z, null) {
      }

      public WorldCoords(WorldCoordUnit units, int x, int z, int relativeToX, int relativeToZ)
         : this(units, x, z, new WorldCoords(units, relativeToX, relativeToZ)) {
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
               break;
         }
         throw new InvalidOperationException(string.Format("WorldCoord conversion not supported from {0} to {1}", fromUnits, toUnits));
      }


      public WorldCoords Offset(int offsetX, int offsetZ) {
         return new WorldCoords(Units, offsetX, offsetZ, this);
         //return new RelativeWorldCoords(this, offsetX, offsetZ);
      }

      public WorldCoords Offset(WorldCoords offset) {
         return new WorldCoords(Units, offset.X, offset.Z, this);
         //return new WorldCoords(Units, X + offset.X, Z + offset.Z, offset.X, offset.Z);
      }


      public string DebugString { get { return string.Format("({0}: {1},{2})", Units, X, Z); } }
   }


   //public sealed class RelativeWorldCoords : WorldCoords {
   //   public WorldCoords RelativeTo { get; private set; }

   //   public RelativeWorldCoords(WorldCoords relativeTo, int offsetX, int offsetZ)
   //      : base(relativeTo.Units, offsetX, offsetZ) {
   //      RelativeTo = relativeTo;
   //   }
   //}



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
