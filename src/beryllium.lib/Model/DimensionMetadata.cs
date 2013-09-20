using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public class DimensionMetadata {
      public DimensionPointer DimensionPointer { get; private set; }

      /// <summary>
      /// May be null if dimension contains no region directory
      /// </summary>
      public RegionPointer[] RegionPointers { get; internal set; }

      public string Name { get { return DimensionPointer.DimensionName; } }
      public bool HasRegions { get { return ( RegionPointers != null ); } }


      public DimensionMetadata(DimensionPointer dimensionPointer) {
         DimensionPointer = dimensionPointer;
      }
   }
}
