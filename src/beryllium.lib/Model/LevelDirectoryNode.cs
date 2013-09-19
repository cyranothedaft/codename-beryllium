using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.lib.Model {
   [DebuggerDisplay("{Name}")]
   public sealed class LevelDirectoryNode {
      private List<LevelDirectoryNode> _children = new List<LevelDirectoryNode>(); 

      public FileSystemInfo FileSystemInfo { get; internal set; }
      public IEnumerable<LevelDirectoryNode> Children { get { return _children; } }


      public string Name {
         get { return FileSystemInfo.Name; }
      }

      public bool IsDirectory {
         get { return FileSystemInfo is DirectoryInfo; }
      }


      public void AddChild(LevelDirectoryNode childNode) {
         _children.Add(childNode);
      }
   }
}
