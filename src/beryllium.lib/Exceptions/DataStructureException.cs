using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Exceptions {
   public abstract class DataStructureException : BerylliumException {
      public DataStructureException() { }
      public DataStructureException(string message) : base(message) { }
      public DataStructureException(string message, Exception inner) : base(message, inner) { }
   }


   public sealed class InvalidLevelStructureException : DataStructureException {
      public InvalidLevelStructureException() { }
      public InvalidLevelStructureException(string message) : base(message) { }
      public InvalidLevelStructureException(string message, Exception inner) : base(message, inner) { }
   }
}
