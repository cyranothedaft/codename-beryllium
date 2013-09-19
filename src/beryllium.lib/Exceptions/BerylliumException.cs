using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Exceptions {
   [Serializable]
   public abstract class BerylliumException : Exception {
      public BerylliumException() { }
      public BerylliumException(string message) : base(message) { }
      public BerylliumException(string message, Exception inner) : base(message, inner) { }
      protected BerylliumException(
       System.Runtime.Serialization.SerializationInfo info,
       System.Runtime.Serialization.StreamingContext context)
         : base(info, context) { }
   }
}
