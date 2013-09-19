using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace beryllium.lib.Nbt {
   internal sealed class NbtTagPayload_List : NbtTagPayload_Composite {
      private readonly NbtTagPayload[] _payloads;

      public TagId ElementType { get; private set; }
      public int Count { get; private set; }


      public NbtTagPayload_List(TagId elementType, int elementCount) {
         Count = elementCount;
         ElementType = elementType;
         _payloads = new NbtTagPayload[elementCount];
      }


      public void SetTagPayload(int index, NbtTagPayload tagPayload) {
         _payloads[index] = tagPayload;
      }


      public override IEnumerable<NbtTagInfo> EnumTags() {
         return _payloads.Select(p => new NbtTagInfo(ElementType, p));
      }


      public override string ToDebugStringShort() {
         return string.Format("[{0}] elements", _payloads.Length);
      }


      public T[] GetArrayOfScalar<T>() {
         if ( !ElementType.IsScalarType() ) {
            throw new InvalidOperationException("GetArrayOfScalar is invalid for " + ElementType.ToString());
         }
         return _payloads.Select(p => ( ( NbtTagPayload_Scalar<T> )p ).GetValue()).ToArray();
      }
   }
}
