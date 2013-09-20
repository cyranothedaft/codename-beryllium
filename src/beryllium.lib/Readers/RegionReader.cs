using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;



namespace beryllium.lib.Readers {
   class RegionReader {
      private readonly RegionPointer _regionPointer;


      public RegionReader(RegionPointer regionPointer) {
         _regionPointer = regionPointer;
      }


      public Region ReadHeader() {
         //TraceWriter.WriteLine(string.Format("Region ( {0,4} , {1,4} )  includes: [ {2,6} , {3,6} ) x [ {4,6} , {5,6} ) chunks",
         //                                    regionInfo.RegionX, regionInfo.RegionZ,
         //                                    Math.Min(regionInfo.ChunkX0, regionInfo.ChunkX1), Math.Max(regionInfo.ChunkX0, regionInfo.ChunkX1),
         //                                    Math.Min(regionInfo.ChunkZ0, regionInfo.ChunkZ1), Math.Max(regionInfo.ChunkZ0, regionInfo.ChunkZ1)));

         Region region = new Region(_regionPointer);

         using ( FileStream fileStream = new FileStream(_regionPointer.FilePath, FileMode.Open, FileAccess.Read) )
         using ( BinaryReader binReader = new BinaryReader(fileStream) ) {
            RegionFileReader rdr = new RegionFileReader(binReader);

            // read location, timestamp, and data for each of 1,024 chunks
            for ( int i = 0; i < 1024; ++i ) {
               // read chunk location and timestamp
               ChunkPointer chunkPtr = rdr.ReadChunkPointer(i);
               region.AddChunkPointer(chunkPtr);
            }
         }

         return region;
      }


      public void ReadChunks(Region region, Action<Chunk> handleChunk) {
         using ( FileStream fileStream = new FileStream(_regionPointer.FilePath, FileMode.Open, FileAccess.Read) )
         using ( BinaryReader binReader = new BinaryReader(fileStream) ) {
            RegionFileReader rdr = new RegionFileReader(binReader);

            foreach ( var chunkPointer in region.ChunkPointers ) {
               Chunk chunk = new Chunk(chunkPointer);
               chunk.Data = rdr.ReadChunkData(chunkPointer);
               handleChunk(chunk);
            }
         }
      }


      //public Chunk ReadChunkHeaderOnly(ChunkPointer chunkPointer) {
      //}
   }
}
