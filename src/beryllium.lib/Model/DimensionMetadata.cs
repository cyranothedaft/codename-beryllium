using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public class DimensionMetadata {
      public DimensionPointer DimensionPointer { get; private set; }
      public RegionPointer[] RegionPointers { get; internal set; }

      public DimensionMetadata(DimensionPointer dimensionPointer) {
         DimensionPointer = dimensionPointer;
      }
   }
}
