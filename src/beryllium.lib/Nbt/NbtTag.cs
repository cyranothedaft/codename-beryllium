using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Nbt {
   [DebuggerDisplay("Id:{TagId}")]
   public class NbtTagInfo {
      public TagId TagId { get; private set; }
      public NbtTagPayload Payload { get; private set; }

      public NbtTagInfo(TagId tagId, NbtTagPayload payload) {
         TagId = tagId;
         Payload = payload;
      }
   }


   [DebuggerDisplay("Id:{TagId} {TagName}")]
   public sealed class NbtTag : NbtTagInfo {
      public string TagName { get; private set; }

      public NbtTag(TagId tagId, string tagName, NbtTagPayload payload)
         : base(tagId, payload) {
         TagName = tagName;
      }
   }


   internal static partial class Extensions {
      public static bool IsScalarType(this TagId tagId) {
         switch ( tagId ) {
            case TagId.TAG_Byte:
            case TagId.TAG_Short:
            case TagId.TAG_Int:
            case TagId.TAG_Long:
            case TagId.TAG_Float:
            case TagId.TAG_Double:
            case TagId.TAG_String:
               return true;
            case TagId.TAG_End:
            case TagId.TAG_Byte_Array:
            case TagId.TAG_List:
            case TagId.TAG_Compound:
            case TagId.TAG_Int_Array:
            default:
               return false;
         }
      }
   }
}
