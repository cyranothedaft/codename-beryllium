using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;
using beryllium.lib.Nbt;



namespace beryllium.lib.Readers {
   class RegionFileReader : BerylliumBinaryReader {
      public RegionFileReader(BinaryReader binReader) : base(binReader) { }


      public ChunkPointer ReadChunkPointer(int chunkIndex) {
         _binReader.BaseStream.Seek(chunkIndex * 4, SeekOrigin.Begin);

         // read location and length
         fillBufferReversed(3);
         int fileSectorOffset = BitConverter.ToInt32(_tempBuffer, 0);
         int fileSectorExtent = _binReader.ReadByte();

         // read timestamp
         _binReader.BaseStream.Seek(4096, SeekOrigin.Current);
         uint timestamp = base.readBigEndian_Int32Unsigned();

         return new ChunkPointer(chunkIndex, fileSectorOffset, fileSectorExtent, timestamp, this);
      }


      public ChunkData ReadChunkData(ChunkPointer chunkPtr) {
         if ( chunkPtr == null || chunkPtr.FileSectorExtent == 0 ) return null;

         _binReader.BaseStream.Seek(chunkPtr.FileSectorOffset * 4096, SeekOrigin.Begin);

         int dataLen = ( int )base.readBigEndian_Int32Unsigned();
         RegionCompressionType compressionType = readCompressionType();

         ChunkData chunkData = null;
         if ( dataLen > 0 ) {
            // when using Deflate (RFC1951) to read ZLib-compressed data, skip first two bytes and last four
            _binReader.BaseStream.Seek(2, SeekOrigin.Current);
            dataLen -= 4;

            // read compressed chunk data into memory
            byte[] compressedData = _binReader.ReadBytes(dataLen);

            chunkData = new ChunkData();
            using ( MemoryStream ms = new MemoryStream(compressedData) )
            using ( DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Decompress) )
            using ( BinaryReader binReader2 = new BinaryReader(deflateStream) ) {
               NbtReader rdr = new NbtReader(binReader2);
               // read root tag (should be single compound tag)
               chunkData.RootTag = rdr.ReadNextTag();
            }
         }
         return chunkData;
      }


      private CompressionType readCompressionType() {
         byte compressionByte = _binReader.ReadByte();
         CompressionType compressionType = ( CompressionType )compressionByte;
         return compressionType;
      }
   }
}
