﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;
using beryllium.lib.Nbt;



namespace beryllium.lib.Readers {
   internal sealed class RegionFileReader : BerylliumBinaryReader {
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

         return new ChunkPointer(chunkIndex, fileSectorOffset, fileSectorExtent, timestamp);
      }


      public NbtTag ReadChunkData(ChunkPointer chunkPtr) {
         if ( chunkPtr == null || chunkPtr.FileSectorExtent == 0 ) return null;

         _binReader.BaseStream.Seek(chunkPtr.FileSectorOffset * 4096, SeekOrigin.Begin);

         int dataLen = ( int )base.readBigEndian_Int32Unsigned();
         RegionCompressionType compressionType = readCompressionType();

         NbtTag chunkDataTag = null;
         if ( dataLen > 0 ) {
            // when using Deflate (RFC1951) to read ZLib-compressed data, skip first two bytes and last four
            _binReader.BaseStream.Seek(2, SeekOrigin.Current);
            dataLen -= 4;

            // read compressed chunk data into memory
            byte[] compressedData = _binReader.ReadBytes(dataLen);

            using ( MemoryStream ms = new MemoryStream(compressedData) )
            using ( DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Decompress) )
            using ( BinaryReader chunkBinReader = new BinaryReader(deflateStream) ) {
               NbtReader rdr = new NbtReader(chunkBinReader);
               // read root tag (should be single compound tag)
               chunkDataTag = rdr.ReadNextTag();
            }
         }
         return chunkDataTag;
      }


      private RegionCompressionType readCompressionType() {
         byte compressionByte = _binReader.ReadByte();
         RegionCompressionType compressionType = ( RegionCompressionType )compressionByte;
         return compressionType;
      }
   }
}
