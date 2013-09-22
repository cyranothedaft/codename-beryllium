﻿using System;
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
         MapGenerator mapGen = new MapGenerator(outputDir, "test - ");
         worldReader.RegisterProcessor(new ConsoleTraceProcessor());
         worldReader.RegisterProcessor(mapGen);
         worldReader.Process(levelFilePath);
      }
   }
}