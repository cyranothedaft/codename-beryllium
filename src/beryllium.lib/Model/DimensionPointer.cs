using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public class DimensionPointer {
      public LevelDirectoryNode DimensionDirectoryNode { get; private set; }
      public string DimensionName { get; private set; }

      public DimensionPointer(LevelDirectoryNode dimensionDirectoryNode, string dimensionName) {
         DimensionDirectoryNode = dimensionDirectoryNode;
         DimensionName = dimensionName;
      }

      public bool Exists { get { return ( DimensionDirectoryNode != null ); } }
   }
}
