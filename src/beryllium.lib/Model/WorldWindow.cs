using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   [DebuggerDisplay("{Location.DebugString} - {Supremum.DebugString}")]
   public class WorldWindow {
      public WorldCoordUnit Units { get; private set; }
      public WorldCoords Location { get; private set; }
      //public WorldCoords SecondCorner { get; private set; }
      public WorldCoords Extent { get; private set; }
      public WorldCoords Supremum { get; private set; }


      private WorldWindow(WorldCoordUnit units, WorldCoords location, WorldCoords extent) {
         Units = units;
         Location = location;
         Extent = extent;
         Supremum = Location.Offset(Extent);
      }


      public WorldWindow(WorldCoordUnit units, int x, int z, int extentX, int extentZ)
         : this(units, new WorldCoords(units, x, z), new WorldCoords(units, extentX, extentZ)) {
      }


      public WorldWindow(WorldCoords location, int extentX, int extentZ)
         : this(location.Units, location, new WorldCoords(location.Units, extentX, extentZ)) {
      }


      //public static WorldCoords ExtentTo(WorldCoords firstCorner, WorldCoords secondCorner) {
      //   return new WorldCoords(firstCorner.Units,
      //                          secondCorner.X - firstCorner.X + 1,
      //                          secondCorner.Z - firstCorner.Z + 1);
      //}


      public static WorldWindow ExtentFromCoordList(IEnumerable<WorldCoords> coords) {
         WorldCoordUnit units = WorldCoordUnit.Block; // will be overwritten in each iteration
         int minX = int.MaxValue,
             minZ = int.MaxValue,
             maxX = int.MinValue,
             maxZ = int.MinValue;

         foreach ( var coord in coords ) {
            units = coord.Units;
            if ( coord.X < minX ) minX = coord.X;
            if ( coord.Z < minZ ) minZ = coord.Z;
            if ( coord.X > maxX ) maxX = coord.X;
            if ( coord.Z > maxZ ) maxZ = coord.Z;
         }

         return new WorldWindow(units, minX, minZ, maxX - minX + 1, maxZ - minZ + 1);
      }


      public WorldWindow ConvertTo(WorldCoordUnit newUnits) {
         WorldWindow newWindow = new WorldWindow(newUnits,
                                                 this.Location.ConvertTo(newUnits),
                                                 this.Extent.ConvertTo(newUnits));
         return newWindow;
      }
   }


   //===
//   public sealed class RelativeWorldWindow : WorldWindow {
//      private RelativeWorldCoords _location;
//
//      public WorldWindow RelativeTo { get; private set; }
//      public override WorldCoords Location { get { return _location; } }
//
//      public RelativeWorldWindow(WorldWindow relativeTo, WorldCoords location, int extentX, int extentZ)
//         : base(relativeTo.Units, extentX, extentZ) {
//         RelativeTo = relativeTo;
//         Location = new RelativeWorldCoords(location;
//      }
//   }


   //public abstract class WorldWindow<T> where T:WorldCoords, new() {
   //   // Note: window coord pairs are inclusive; i.e. width == max - min + 1
   //   public T Min, Max;

   //   protected WorldWindow() { }

   //   protected WorldWindow(int minX, int minZ, int maxX, int maxZ) {
   //      Min = new T() { X = minX, Z = minZ };
   //      Max = new T() { X = maxX, Z = maxZ };
   //   }
   //}


   //public sealed class WorldWindow_Region : WorldWindow<WorldCoords_Region> {
   //   public WorldWindow_Region(int minX, int minZ, int maxX, int maxZ)
   //      : base(minX, minZ, maxX, maxZ) {
   //   }


   //   private WorldWindow_Region() { }


   //   public WorldWindow_Region(WorldCoords_Region startingPoint, int width, int height) {
   //      Min = startingPoint;
   //      Max = startingPoint.Offset(width, height);
   //   }


   //   public static WorldWindow_Region ExtentFromCoordList(IEnumerable<WorldCoords_Region> regionCoords) {
   //      WorldCoords_Region min = new WorldCoords_Region(),
   //                         max = new WorldCoords_Region();

   //      min.X = int.MaxValue;
   //      min.Z = int.MaxValue;
   //      max.X = int.MinValue;
   //      max.Z = int.MinValue;
   //      foreach ( var regionCoord in regionCoords ) {
   //         if ( regionCoord.X < min.X ) min.X = regionCoord.X;
   //         if ( regionCoord.Z < min.Z ) min.Z = regionCoord.Z;
   //         if ( regionCoord.X > max.X ) max.X = regionCoord.X;
   //         if ( regionCoord.Z > max.Z ) max.Z = regionCoord.Z;
   //      }
   //      return new WorldWindow_Region() { Min = min, Max = max };
   //   }
   //}


   //public sealed class WorldWindow_Block : WorldWindow<WorldCoords_Block> {
   //   public WorldWindow_Block(int minX, int minZ, int maxX, int maxZ)
   //      : base(minX, minZ, maxX, maxZ) {
   //   }

   //   public WorldWindow_Block(WorldWindow_Region window_region)
   //      : base(WorldCoords.RegionToBlockFactor * window_region.Min.X,
   //             WorldCoords.RegionToBlockFactor * window_region.Min.Z,
   //             WorldCoords.RegionToBlockFactor * window_region.Max.X,
   //             WorldCoords.RegionToBlockFactor * window_region.Max.Z) {
   //   }
   //}
}
