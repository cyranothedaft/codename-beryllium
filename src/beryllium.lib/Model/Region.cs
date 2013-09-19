using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public class Region {
      public RegionHeader HeaderData {
         get { throw new NotImplementedException(); }
      }

      public IEnumerable<ChunkPointer> ChunkPointers {
         get { throw new NotImplementedException(); }
      }
   }
}
