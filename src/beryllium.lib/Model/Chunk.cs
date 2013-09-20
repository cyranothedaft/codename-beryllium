using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Nbt;



namespace beryllium.lib.Model {
   public class Chunk {
      public ChunkPointer ChunkPointer { get; internal set; }
      public NbtTag Data { get; internal set; }

      public Chunk(ChunkPointer chunkPointer) {
         ChunkPointer = chunkPointer;
      }
   }
}
