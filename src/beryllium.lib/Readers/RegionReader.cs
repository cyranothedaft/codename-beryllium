using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;



namespace beryllium.lib.Readers {
   class RegionReader {
      public RegionReader(RegionPointer regionPointer) {
         throw new NotImplementedException();
      }


      public Region ReadHeaderOnly() {
         throw new NotImplementedException();
      }


      public void ReadData(RegionPointer regionPointer, Region readIntoRegion) {
         throw new NotImplementedException();
      }


      public Chunk ReadChunkHeaderOnly(ChunkPointer chunkPointer) {
         throw new NotImplementedException();
      }


      public void ReadChunkData(ChunkPointer chunkPointer, Chunk readIntoChunk) {
         throw new NotImplementedException();
      }
   }
}
