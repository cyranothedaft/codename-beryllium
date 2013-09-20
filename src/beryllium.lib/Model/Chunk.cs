using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Nbt;



namespace beryllium.lib.Model {
   public class Chunk {
      public ChunkHeader Header {
         get { throw new NotImplementedException(); }
      }

      internal NbtTag Data { get; set; }
   }
}
