using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using beryllium.lib.Exceptions;



namespace beryllium.lib.Model {
   [DebuggerDisplay("{FileName}")]
   public class RegionPointer {
      private readonly FileInfo _regionFileInfo;

      //public int RegionX, RegionZ;
      //public int ChunkX0, ChunkZ0;
      //public int ChunkX1, ChunkZ1;

      public string FileName { get { return _regionFileInfo.Name; } }
      public string FilePath { get { return _regionFileInfo.FullName; } }

      /// <summary>
      /// Absolute region coords of region
      /// </summary>
      public WorldCoords RegionCoords { get; private set; }


      public RegionPointer(FileInfo regionFileInfo) {
         _regionFileInfo = regionFileInfo;

         Regex regionNameRe = new Regex(@"^r\.(?'X'[-0-9]+)\.(?'Z'[-0-9]+)\.mca$", RegexOptions.ExplicitCapture);
         Match m = regionNameRe.Match(_regionFileInfo.Name);
         if ( !m.Success ) throw new InvalidLevelStructureException(string.Format("Invalid region filename format: \"{0}\"", _regionFileInfo.FullName));

         RegionCoords = new WorldCoords(WorldCoordUnit.Region,
                                        int.Parse(m.Groups["X"].Value),
                                        int.Parse(m.Groups["Z"].Value));
         //ChunkX0 = RegionX << 5;
         //ChunkZ0 = RegionZ << 5;
         //int extentX = ( regionX < 0 ) ? -1 : 1,
         //    extentZ = ( regionZ < 0 ) ? -1 : 1;
         //ChunkX1 = ( RegionX + 1 ) << 5;
         //ChunkZ1 = ( RegionZ + 1 ) << 5;
         //int chunkX1 = ( regionX + extentX ) << 5,
         //    chunkZ1 = ( regionZ + extentZ ) << 5;
      }
   }
}
