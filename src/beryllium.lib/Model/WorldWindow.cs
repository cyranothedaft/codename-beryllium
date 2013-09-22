using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public abstract class WorldWindow<T> where T:WorldCoords, new() {
      // Note: window coord pairs are inclusive; i.e. width == max - min + 1
      public T Min, Max;

      protected WorldWindow() { }

      protected WorldWindow(int minX, int minZ, int maxX, int maxZ) {
         Min = new T() { X = minX, Z = minZ };
         Max = new T() { X = maxX, Z = maxZ };
      }
   }


   public sealed class WorldWindow_Region : WorldWindow<WorldCoords_Region> {
      public WorldWindow_Region(int minX, int minZ, int maxX, int maxZ)
         : base(minX, minZ, maxX, maxZ) {
      }


      private WorldWindow_Region() { }


      public WorldWindow_Region(WorldCoords_Region startingPoint, int width, int height) {
         Min = startingPoint;
         Max = startingPoint.Offset(width, height);
      }


      public static WorldWindow_Region ExtentFromCoordList(IEnumerable<WorldCoords_Region> regionCoords) {
         WorldCoords_Region min = new WorldCoords_Region(),
                            max = new WorldCoords_Region();

         min.X = int.MaxValue;
         min.Z = int.MaxValue;
         max.X = int.MinValue;
         max.Z = int.MinValue;
         foreach ( var regionCoord in regionCoords ) {
            if ( regionCoord.X < min.X ) min.X = regionCoord.X;
            if ( regionCoord.Z < min.Z ) min.Z = regionCoord.Z;
            if ( regionCoord.X > max.X ) max.X = regionCoord.X;
            if ( regionCoord.Z > max.Z ) max.Z = regionCoord.Z;
         }
         return new WorldWindow_Region() { Min = min, Max = max };
      }
   }


   public sealed class WorldWindow_Block : WorldWindow<WorldCoords_Block> {
      public WorldWindow_Block(int minX, int minZ, int maxX, int maxZ)
         : base(minX, minZ, maxX, maxZ) {
      }

      public WorldWindow_Block(WorldWindow_Region window_region)
         : base(WorldCoords.RegionToBlockFactor * window_region.Min.X,
                WorldCoords.RegionToBlockFactor * window_region.Min.Z,
                WorldCoords.RegionToBlockFactor * window_region.Max.X,
                WorldCoords.RegionToBlockFactor * window_region.Max.Z) {
      }
   }
}
