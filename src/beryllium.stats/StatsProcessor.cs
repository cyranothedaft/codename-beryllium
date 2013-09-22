using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib;
using beryllium.lib.Model;



namespace beryllium.stats {
   public class StatsProcessor : IWorldProcessor {
      private CounterContainerNode _currentLevel;
      private CounterContainerNode _currentDimension;
      private CounterContainerNode _currentRegion;
      private CounterContainerNode _currentChunk;


      public void ProcessLevelDirectory(LevelDirectoryMetadata levelDirectoryMetadata) {
         //
      }


      public void ProcessLevelMetadata(LevelMetadata levelMetadata) {
         _currentLevel = new CounterContainerNode(NodeType.Level, levelMetadata.LevelName);
      }


      public void ProcessDimensionMetadata(DimensionMetadata dimension) {
         _currentDimension = _currentLevel.AddChild(NodeType.Dimension, dimension.Name);
         _currentDimension.Counters.Dimensions = 1;
         _currentLevel.Counters.Dimensions++;
      }


      public void ProcessRegionHeader(Region region) {
         _currentRegion = _currentDimension.AddChild(NodeType.Region, region.RegionPointer.FileName);
         _currentRegion.Counters.Regions = 1;
         _currentDimension.Counters.Regions++;
         _currentLevel.Counters.Regions++;
      }


      public void ProcessChunk(Chunk chunk) {
         if ( chunk.HasData ) {
            _currentChunk = _currentRegion.AddChild(NodeType.Chunk, chunk.ChunkPointer.ChunkIndex.ToString("0000"));
            _currentChunk.Counters.Chunks = 1;
            _currentRegion.Counters.Chunks++;
            _currentDimension.Counters.Chunks++;
            _currentLevel.Counters.Chunks++;
         }
      }


      //public void ProcessChunkHeader(ChunkHeader chunkHeader) {
      //   _currentChunk = _currentRegion.AddChild(NodeType.Chunk, chunkHeader.Index.ToString("0000"));
      //   _currentChunk.Counters.Chunks = 1;
      //   _currentRegion.Counters.Chunks++;
      //   _currentDimension.Counters.Chunks++;
      //   _currentLevel.Counters.Chunks++;
      //}


      //public void ProcessChunkData(Chunk chunk) {
      //   throw new NotImplementedException();
      //}


      public void ProcessRegionEnd(Region region) {
         _currentRegion = null;
      }


      public void ProcessDimensionEnd(DimensionMetadata dimension) {
         _currentDimension = null;
      }


      public void ProcessLevelEnd(LevelMetadata levelMetadata) {
         //
      }


      public string GetCsv() {
         StringBuilder sb = new StringBuilder();
         sb.AppendLine("level,dim,region,dims,regions,chunks");
         addRowsForNode(sb, _currentLevel);

         return sb.ToString();
      }


      private void addRowsForNode(StringBuilder sb, CounterContainerNode node) {
         for ( int i = 0; i < 3; ++i ) {
            if ( i == ( int )node.Type ) sb.Append(node.Name);
            sb.Append(",");
         }
         sb.Append(node.Counters.Dimensions);
         sb.Append(",");
         sb.Append(node.Counters.Regions);
         sb.Append(",");
         sb.Append(node.Counters.Chunks);
         sb.AppendLine();

         foreach ( var childNode in node.Children.Where(c => c.Type != NodeType.Chunk) )
            addRowsForNode(sb, childNode);
      }
   }
}
