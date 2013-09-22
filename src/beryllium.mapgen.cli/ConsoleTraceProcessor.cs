using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib;
using beryllium.lib.Model;



namespace beryllium.mapgen.cli {
   internal sealed class ConsoleTraceProcessor : IWorldProcessor {
      private int _indent = 0;
      private string _indentStr = "";

      private int Indent {
         get { return _indent; }
         set {
            _indent = value;
            _indentStr = new string(' ', _indent * 3);
         }
      }


      public void ProcessLevelDirectory(LevelDirectoryMetadata levelDirectoryMetadata) {
         Console.WriteLine("Level directory:  {0}", levelDirectoryMetadata.LevelDatFilePath);
      }


      public void ProcessLevelMetadata(LevelMetadata levelMetadata) {
         Console.WriteLine("Level metadata - {0}", levelMetadata.LevelName);
         Console.WriteLine("   Time:       {0}", levelMetadata.Time);
         Console.WriteLine("   DayTime:    {0}", levelMetadata.DayTime);
         Console.WriteLine("   LastPlayed: {0}", levelMetadata.LastPlayed);
      }


      public void ProcessDimensionMetadata(DimensionMetadata dimension) {
         Console.WriteLine("Dimension - {0}", dimension.Name);
         Console.WriteLine("   Exists:    {0}", dimension.DimensionPointer.Exists);
         if ( dimension.DimensionPointer.Exists ) {
            Console.WriteLine("   Directory: {0}", dimension.DimensionPointer.DimensionDirectoryNode.Name);
            Console.WriteLine("   Regions:   {0}", dimension.HasRegions ? dimension.RegionPointers.Count().ToString("N0") : "none");
         }
         Indent = 1;
      }


      public void ProcessRegionHeader(Region region) {
         Console.WriteLine("{0}Region - {1}", _indentStr, region.RegionPointer.FileName);
         ++Indent;
         Console.Write("{0}Chunks ", _indentStr);
      }


      public void ProcessChunk(Chunk chunk) {
         Console.Write(chunk.HasData ? ":" : ".");
      }


      public void ProcessRegionEnd(Region region) {
         Console.WriteLine();
         --Indent;
      }


      public void ProcessDimensionEnd(DimensionMetadata dimension) {
         //
      }


      public void ProcessLevelEnd(LevelMetadata levelMetadata) {
         //
      }
   }
}
