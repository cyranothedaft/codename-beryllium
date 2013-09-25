using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Readers;



namespace beryllium.lib.Model {
   [DebuggerDisplay("#{ChunkIndex}")]
   public class ChunkPointer {
      public int ChunkIndex { get; set; }
      public int FileSectorOffset { get; set; }
      public int FileSectorExtent { get; set; }
      public uint Timestamp { get; set; }


      internal ChunkPointer(int chunkIndex, int fileSectorOffset, int fileSectorExtent, uint timestamp) {
         ChunkIndex = chunkIndex;
         FileSectorOffset = fileSectorOffset;
         FileSectorExtent = fileSectorExtent;
         Timestamp = timestamp;
      }
   }
}
