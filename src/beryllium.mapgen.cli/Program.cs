using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib;



namespace beryllium.mapgen.cli {
   class Program {
      private static void Main(string[] args) {
         string levelFilePath = args[0];
         string outputDir = args[1];

         WorldReader worldReader = new WorldReader();
         HeightMapGenerator heightMapGen = new HeightMapGenerator(outputDir, "test - ");
         worldReader.RegisterProcessor(new ConsoleTraceProcessor());
         worldReader.RegisterProcessor(heightMapGen);
         worldReader.Process(levelFilePath);

         Console.WriteLine("Generated {0} files:", heightMapGen.GeneratedFiles.Count);
         foreach ( string file in heightMapGen.GeneratedFiles )
            Console.WriteLine(" - {0}", new FileInfo(file).FullName);
      }
   }
}
