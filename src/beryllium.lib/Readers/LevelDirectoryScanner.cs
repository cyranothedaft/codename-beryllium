using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;
using beryllium.lib.Nbt;



namespace beryllium.lib.Readers {
   internal sealed class LevelDirectoryScanner {
      private readonly string _levelDatFilePath;
      private readonly string _levelDirectoryPath;


      /// <summary>
      /// 
      /// </summary>
      /// <param name="levelFilePath">Refers either to a level.dat file or a world level directory that contains a level.dat file.</param>
      public LevelDirectoryScanner(string levelFilePath) {
         // TODO ? read session.lock file and issue suitable warning

         if ( !string.Equals(Path.GetFileName(levelFilePath), LevelDatReader.LevelDatFileName, StringComparison.OrdinalIgnoreCase) )
            levelFilePath = Path.Combine(levelFilePath, LevelDatReader.LevelDatFileName);

         if ( !File.Exists(levelFilePath) )
            throw new FileNotFoundException(string.Concat(LevelDatReader.LevelDatFileName, " not found at specified path: ", levelFilePath), levelFilePath);

         _levelDatFilePath = levelFilePath;
         _levelDirectoryPath = Path.GetDirectoryName(levelFilePath);
      }


      public LevelDirectoryMetadata Scan() {
         LevelDirectoryMetadata levelDirectoryMetadata = new LevelDirectoryMetadata()
            {
               LevelDatFilePath = _levelDatFilePath,
               LevelDirectoryPath = _levelDirectoryPath,
            };

         // traverse directory structure and enumerate names of files and directories
         levelDirectoryMetadata.FileSystemDirectory = getDirNode(new DirectoryInfo(_levelDirectoryPath));

         // get list of dimensions by scanning in-memory copy of directory structure
         levelDirectoryMetadata.Dimensions = findDimensions(levelDirectoryMetadata.FileSystemDirectory).ToArray();

         return levelDirectoryMetadata;
      }


      private LevelDirectoryNode getDirNode(DirectoryInfo dirInfo) {
         LevelDirectoryNode dirNode = getFileSystemNode(dirInfo);

         foreach ( FileSystemInfo childInfo in dirInfo.GetFileSystemInfos() ) {
            DirectoryInfo childDirInfo = childInfo as DirectoryInfo;
            LevelDirectoryNode childNode = ( childDirInfo != null )
                                              ? getDirNode(childDirInfo)
                                              : getFileSystemNode(childInfo);
            dirNode.AddChild(childNode);
         }

         return dirNode;
      }


      private LevelDirectoryNode getFileSystemNode(FileSystemInfo fsInfo) {
         return new LevelDirectoryNode() { FileSystemInfo = fsInfo };
      }


      private IEnumerable<DimensionPointer> findDimensions(LevelDirectoryNode fileSystemDirectory) {
         yield return findDimension(fileSystemDirectory, "region", "Overworld");
         yield return findDimension(fileSystemDirectory, "DIM-1", "The Nether");
         yield return findDimension(fileSystemDirectory, "DIM1", "The End");
         // TODO: support more dimensions, such as Twilight Portal and Mystcraft ages
      }


      private DimensionPointer findDimension(LevelDirectoryNode rootDirNode, string dimDirName, string dimName) {
         LevelDirectoryNode dimDirNode = rootDirNode.Children.FirstOrDefault(d => d.Name == dimDirName);
         DimensionPointer dimPointer = new DimensionPointer(dimDirNode, dimName);
         return dimPointer;
      }
   }
}
