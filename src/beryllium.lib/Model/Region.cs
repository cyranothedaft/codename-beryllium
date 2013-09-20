using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public class Region {
      private readonly RegionPointer _regionPointer;
      private readonly List<ChunkPointer> _chunkPointers = new List<ChunkPointer>();


      public Region(RegionPointer regionPointer) {
         _regionPointer = regionPointer;
      }


      public void AddChunkPointer(ChunkPointer chunkPtr) {
         _chunkPointers.Add(chunkPtr);
      }


      public IEnumerable<ChunkPointer> ChunkPointers { get { return _chunkPointers; } }
   }
}
