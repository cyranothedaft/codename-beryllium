using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;



namespace beryllium.lib.Readers {
   internal static class DimensionScanner {
      public static DimensionMetadata ReadMetadata(DimensionPointer dimensionPointer) {
         var regionFiles = findRegionFiles(dimensionPointer);
         var regionPointers = regionFiles.Select(f => new RegionPointer(f));
         DimensionMetadata dimensionMetadata = new DimensionMetadata(dimensionPointer)
                                                  {
                                                     RegionPointers = regionPointers.ToArray()
                                                  };
         return dimensionMetadata;
      }


      private static IEnumerable<FileInfo> findRegionFiles(DimensionPointer dimensionPointer) {
         string dimRootDir = dimensionPointer.DimensionDirectoryNode.FileSystemInfo.FullName;
         string regionDir = Path.Combine(dimRootDir, FileSystemNames.DirName_Region);
         return new DirectoryInfo(regionDir).EnumerateFiles(FileSystemNames.RegionFileSpec);
      }
   }
}
