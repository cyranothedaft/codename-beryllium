using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Exceptions;
using beryllium.lib.Nbt;



namespace beryllium.lib.Model {
   public class Chunk {
      private NbtTag _data;

      public ChunkPointer ChunkPointer { get; internal set; }

      internal NbtTag Data { get { return _data; } }

      /// <summary>
      /// Absolute block coords of chunk
      /// </summary>
      public WorldCoords ChunkCoords { get; private set; }

      //public int ChunkPosX { get; private set; }
      //public int ChunkPosZ { get; private set; }
      public int[] HeightValues { get; private set; }


      public Chunk(ChunkPointer chunkPointer) {
         ChunkPointer = chunkPointer;
      }


      public bool HasData { get { return ( _data != null ); } }


      internal void SetData(NbtTag rootTag) {
         NbtTag heightMapTag = findNbtTag(rootTag, "Level", "HeightMap");
         if ( heightMapTag == null ) throw new InvalidLevelStructureException("HeightMap tag not found in chunk data");

         NbtTagPayload_List mapPayload = heightMapTag.Payload as NbtTagPayload_List;
         HeightValues = mapPayload.GetArrayOfScalar<int>();

         NbtTag xPosTag = findNbtTag(rootTag, "Level", "xPos");
         NbtTag zPosTag = findNbtTag(rootTag, "Level", "zPos");

         ChunkCoords = new WorldCoords(WorldCoordUnit.Chunk,
                                       ( ( NbtTagPayload_Scalar<int> )xPosTag.Payload ).GetValue(),
                                       ( ( NbtTagPayload_Scalar<int> )zPosTag.Payload ).GetValue());

         _data = rootTag;
      }


      private NbtTag findNbtTag(NbtTag startTag, params string[] tagNamePath) {
         NbtTag targetTag = null;
         NbtTag parent = startTag;
         foreach ( string tagName in tagNamePath ) {
            if ( parent == null ) return null;
            NbtTagPayload_Compound parentPayload = parent.Payload as NbtTagPayload_Compound;
            if ( parentPayload == null ) return null;
            targetTag = parentPayload.Tags.FirstOrDefault(t => t.TagName == tagName);
            if ( targetTag == null ) return null;
            parent = targetTag;
         }
         return targetTag;
      }
   }
}
