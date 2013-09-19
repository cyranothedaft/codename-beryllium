using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using beryllium.lib.Exceptions;



namespace beryllium.lib.Nbt {
   internal sealed class NbtTagPayload_Compound : NbtTagPayload_Composite {
      private readonly List<NbtTag> _innerTags = new List<NbtTag>();

      public IEnumerable<NbtTag> Tags { get { return _innerTags; } } 

      public void AddTag(NbtTag innerTag) {
         _innerTags.Add(innerTag);
      }


      public override IEnumerable<NbtTagInfo> EnumTags() {
         return _innerTags;
      }


      public T GetScalarTagValue<T>(string tagName) {
         var tag = _innerTags.FirstOrDefault(t => t.TagName == tagName);
         if ( tag == null ) throw new InvalidLevelStructureException(string.Format("Tag not found: \"{0}\".", tagName));
         var scalarPayload = tag.Payload as NbtTagPayload_Scalar<T>;
         if ( scalarPayload == null ) throw new InvalidLevelStructureException(string.Format("Tag payload is not the expected scalar type ({0}): \"{1}\" : {2}.", typeof( T ), tagName, tag.Payload.GetType().Name));
         return scalarPayload.GetValue();
      }

      public NbtTagPayload_Compound GetCompoundTagPayload(string tagName) {
         var tag = _innerTags.FirstOrDefault(t => t.TagName == tagName);
         if ( tag == null ) throw new InvalidLevelStructureException(string.Format("Tag not found: \"{0}\".", tagName));
         var compoundPayload = tag.Payload as NbtTagPayload_Compound;
         if ( compoundPayload == null ) throw new InvalidLevelStructureException(string.Format("Tag payload is not compound: \"{0}\" : {1}.", tagName, tag.Payload.GetType().Name));
         return compoundPayload;
      }


      public override string ToDebugStringShort() {
         return string.Format("[{0}] tags", _innerTags.Count);
      }
   }
}
