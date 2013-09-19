using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Nbt {
   internal abstract class NbtBinaryReader {
      protected readonly BinaryReader _binReader;

      protected NbtBinaryReader(BinaryReader binReader) {
         _binReader = binReader;
      }


      protected readonly byte[] _tempBuffer = new byte[16];

      protected short readBigEndian_Int16Signed() {
         fillBufferReversed(2);
         return BitConverter.ToInt16(_tempBuffer, 0);
      }


      protected ushort readBigEndian_Int16Unsigned() {
         fillBufferReversed(2);
         return BitConverter.ToUInt16(_tempBuffer, 0);
      }


      protected int readBigEndian_Int32Signed() {
         fillBufferReversed(4);
         return BitConverter.ToInt32(_tempBuffer, 0);
      }


      protected uint readBigEndian_Int32Unsigned() {
         fillBufferReversed(4);
         return BitConverter.ToUInt32(_tempBuffer, 0);
      }


      protected long readBigEndian_Int64Signed() {
         fillBufferReversed(8);
         return BitConverter.ToInt64(_tempBuffer, 0);
      }


      protected float readBigEndian_Float32_IEEE754() {
         fillBufferReversed(4);
         return BitConverter.ToSingle(_tempBuffer, 0);
      }


      protected double readBigEndian_Float64_IEEE754() {
         fillBufferReversed(8);
         return BitConverter.ToDouble(_tempBuffer, 0);
      }


      protected void fillBufferReversed(int cnt) {
         // initialize _tempBuffer with 0
         _tempBuffer.Initialize();

         for ( int i = cnt - 1; i >= 0; --i )
            _tempBuffer[i] = _binReader.ReadByte();
      }


      protected void readAndIgnore(int cnt) {
         _binReader.ReadBytes(cnt);
      }
   }
}
