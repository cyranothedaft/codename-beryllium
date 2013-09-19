using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   public sealed class LevelDirectoryMetadata {
      public string LevelDatFilePath { get; internal set; }
      public string LevelDirectoryPath { get; internal set; }
      public LevelDirectoryNode FileSystemDirectory { get; internal set; }
      public DimensionPointer[] DimensionPointers { get; internal set; }
   }
}
