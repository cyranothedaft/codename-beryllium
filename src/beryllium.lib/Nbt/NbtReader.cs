using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Nbt {
   internal enum TagId : int {
      TAG_End = 0,
      TAG_Byte,
      TAG_Short,
      TAG_Int,
      TAG_Long,
      TAG_Float,
      TAG_Double,
      TAG_Byte_Array,
      TAG_String,
      TAG_List,
      TAG_Compound,
      TAG_Int_Array,
   }


   internal sealed class NbtReader : NbtBinaryReader {
      public NbtReader(BinaryReader binReader) : base(binReader) { }


      public NbtTag ReadNextTag() {
         // read NBT tag
         TagId tagId = readTagId();

         // read tag name
         string tagName = null;
         switch ( tagId ) {
            case TagId.TAG_End:
               // no name
               break;
            case TagId.TAG_Byte:
            case TagId.TAG_Short:
            case TagId.TAG_Int:
            case TagId.TAG_Long:
            case TagId.TAG_Float:
            case TagId.TAG_Double:
            case TagId.TAG_Byte_Array:
            case TagId.TAG_String:
            case TagId.TAG_List:
            case TagId.TAG_Compound:
            case TagId.TAG_Int_Array:
               tagName = readTagName();
               break;
            default:
               tagName = null;
               break;
         }

         // read tag payload
         NbtTagPayload payload = readPayload(tagId);

         NbtTag tag = new NbtTag(tagId, tagName, payload);
         //TraceWriter.WriteNbtTag(tag);
         return tag;
      }


      private TagId readTagId() {
         byte tagType = _binReader.ReadByte();
         TagId tagId = ( TagId )tagType;
         return tagId;
      }


      private NbtTagPayload readPayload(TagId tagId) {
         NbtTagPayload payload = null;
         switch ( tagId ) {
            case TagId.TAG_End:
               payload = new NbtTagPayload_End();
               break;
            case TagId.TAG_Byte:
               payload = this.ReadTagPayload_Byte();
               break;
            case TagId.TAG_Short:
               payload = this.ReadTagPayload_Short();
               break;
            case TagId.TAG_Int:
               payload = this.ReadTagPayload_Int();
               break;
            case TagId.TAG_Long:
               payload = this.ReadTagPayload_Long();
               break;
            case TagId.TAG_Float:
               payload = this.ReadTagPayload_Float();
               break;
            case TagId.TAG_Double:
               payload = this.ReadTagPayload_Double();
               break;
            case TagId.TAG_Byte_Array:
               payload = this.ReadTagPayload_ByteArray();
               break;
            case TagId.TAG_String:
               payload = this.ReadTagPayload_String();
               break;
            case TagId.TAG_List:
               payload = this.ReadTagPayload_List();
               break;
            case TagId.TAG_Compound:
               payload = this.ReadTagPayload_Compound();
               break;
            case TagId.TAG_Int_Array:
               payload = this.ReadTagPayload_IntArray();
               break;
            default:
               Console.WriteLine("! Tag ID " + tagId.ToString() + " (" + ( ( int )tagId ) + ") is not yet implemented.");
               break;
         }
         return payload;
      }


      private NbtTagPayload ReadTagPayload_Byte() {
         sbyte value = _binReader.ReadSByte();
         return new NbtTagPayload_Byte(value);
      }


      private NbtTagPayload ReadTagPayload_Short() {
         short value = readBigEndian_Int16Signed();
         return new NbtTagPayload_Short(value);
      }


      private NbtTagPayload ReadTagPayload_Int() {
         int value = readBigEndian_Int32Signed();
         return new NbtTagPayload_Int(value);
      }


      private NbtTagPayload ReadTagPayload_Long() {
         long value = readBigEndian_Int64Signed();
         return new NbtTagPayload_Long(value);
      }


      private NbtTagPayload ReadTagPayload_Float() {
         float value = readBigEndian_Float32_IEEE754();
         return new NbtTagPayload_Float(value);
      }


      private NbtTagPayload ReadTagPayload_Double() {
         double value = readBigEndian_Float64_IEEE754();
         return new NbtTagPayload_Double(value);
      }


      private NbtTagPayload ReadTagPayload_String() {
         // read string length
         short stringLength = readBigEndian_Int16Signed();

         byte[] stringBytes = _binReader.ReadBytes(stringLength);
         string value = Encoding.UTF8.GetString(stringBytes);
         return new NbtTagPayload_String(value);
      }


      private NbtTagPayload ReadTagPayload_ByteArray() {
         int elementCount = readBigEndian_Int32Signed();

         NbtTagPayload_List array = new NbtTagPayload_List(TagId.TAG_Byte, elementCount);

         //TraceWriter.Indent();
         for ( int i = 0; i < elementCount; ++i ) {
            NbtTagPayload payload = ReadTagPayload_Byte();
            array.SetTagPayload(i, payload);
         }
         //TraceWriter.Outdent();

         return array;
      }


      private NbtTagPayload ReadTagPayload_IntArray() {
         int elementCount = readBigEndian_Int32Signed();

         NbtTagPayload_List array = new NbtTagPayload_List(TagId.TAG_Int, elementCount);

         //TraceWriter.Indent();
         for ( int i = 0; i < elementCount; ++i ) {
            NbtTagPayload payload = ReadTagPayload_Int();
            array.SetTagPayload(i, payload);
         }
         //TraceWriter.Outdent();

         return array;
      }


      private NbtTagPayload ReadTagPayload_List() {
         TagId elementType = readTagId();
         int elementCount = readBigEndian_Int32Signed();

         NbtTagPayload_List list = new NbtTagPayload_List(elementType, elementCount);

         //TraceWriter.Indent();
         for ( int i = 0; i < elementCount; ++i ) {
            NbtTagPayload payload = readPayload(elementType);
            list.SetTagPayload(i, payload);
         }
         //TraceWriter.Outdent();

         return list;
      }


      private NbtTagPayload ReadTagPayload_Compound() {
         NbtTagPayload_Compound compound = new NbtTagPayload_Compound();
         bool isMore;
         //TraceWriter.Indent();
         do {
            NbtTag innerTag = ReadNextTag();
            isMore = ( innerTag != null && innerTag.TagId != TagId.TAG_End );
            if ( isMore )
               compound.AddTag(innerTag);
         } while ( isMore );
         //TraceWriter.Outdent();

         return compound;
      }


      private string readTagName() {
         // read tag name length
         ushort tagNameLen = readBigEndian_Int16Unsigned();

         string tagName = null;
         if ( tagNameLen > 0 ) {
            // read tag name in UTF-8
            byte[] tagNameBytes = _binReader.ReadBytes(tagNameLen);
            tagName = Encoding.UTF8.GetString(tagNameBytes);
         }
         return tagName;
      }
   }
}
