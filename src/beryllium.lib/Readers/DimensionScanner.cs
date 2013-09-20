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
         DimensionMetadata dimensionMetadata = new DimensionMetadata(dimensionPointer);

         var regionFiles = findRegionFiles(dimensionPointer);
         if ( regionFiles != null ) {
            var regionPointers = regionFiles.Select(f => new RegionPointer(f));
            dimensionMetadata.RegionPointers = regionPointers.ToArray();
         }
         return dimensionMetadata;
      }


      private static IEnumerable<FileInfo> findRegionFiles(DimensionPointer dimensionPointer) {
         string dimRootDir = dimensionPointer.DimensionDirectoryNode.FileSystemInfo.FullName;
         string regionDir = Path.Combine(dimRootDir, FileSystemNames.DirName_Region);
         var regionDirInfo = new DirectoryInfo(regionDir);

         return regionDirInfo.Exists
                   ? regionDirInfo.EnumerateFiles(FileSystemNames.RegionFileSpec)
                   : null;
      }
   }
}
