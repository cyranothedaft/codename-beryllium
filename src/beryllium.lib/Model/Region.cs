using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public class Region {
      private readonly List<ChunkPointer> _chunkPointers = new List<ChunkPointer>();

      public RegionPointer RegionPointer { get; private set; }


      public Region(RegionPointer regionPointer) {
         RegionPointer = regionPointer;
      }


      internal void AddChunkPointer(ChunkPointer chunkPtr) {
         _chunkPointers.Add(chunkPtr);
      }


      public IEnumerable<ChunkPointer> ChunkPointers { get { return _chunkPointers; } }
   }
}
