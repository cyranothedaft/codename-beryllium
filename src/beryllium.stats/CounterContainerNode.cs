using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.stats {
   internal enum NodeType {
      Level,
      Dimension,
      Region,
      Chunk
   }


   internal sealed class CounterContainerNode {
      private readonly List<CounterContainerNode> _childNodes = new List<CounterContainerNode>();

      public NodeType Type { get; private set; }
      public string Name { get; set; }
      public Counters Counters { get; private set; }

      public List<CounterContainerNode> Children { get { return _childNodes; } } 


      public CounterContainerNode(NodeType type, string name) {
         Type = type;
         Name = name;
         Counters = new Counters();
      }


      public CounterContainerNode AddChild(NodeType nodeType, string name) {
         CounterContainerNode newNode = new CounterContainerNode(nodeType, name);
         _childNodes.Add(newNode);
         return newNode;
      }
   }
}
