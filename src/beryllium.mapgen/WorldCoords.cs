//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace beryllium.mapgen {
//   [DebuggerDisplay("({X},{Z})")]
//   internal sealed class WorldCoords {
//      public int X { get; set; }
//      public int Z { get; set; }


//      public WorldCoords(int x, int z) {
//         X = x;
//         Z = z;
//      }


//      public static WorldCoords FromRegion(int regionX, int regionZ) {
//         return new WorldCoords(regionX * 32*16, regionZ * 32*16);
//      }


//      public static WorldCoords FromRegionAndChunk(int regionX, int regionZ, int chunkX, int chunkZ) {
//         return new WorldCoords(( regionX * 32 + chunkX ) * 16, ( regionZ * 32 + chunkZ ) * 16);
//      }


//      public static WorldCoords FromChunkAbs(int chunkX, int chunkZ) {
//         return new WorldCoords(chunkX * 16, chunkZ * 16);
//      }


//      public WorldCoords Add(int x, int z) {
//         return new WorldCoords(this.X + x, this.Z + z);
//      }
//   }
//}
