using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib {
   public interface IWorldProcessor {
      void ProcessLevelMetadata();

      void ProcessDimensionMetadata();

      void ProcessRegionHeader();

      void ProcessChunkHeader();

      void ProcessChunkData();

      void ProcessRegionEnd();

      void ProcessDimensionEnd();

      void ProcessLevelEnd();
   }
}
