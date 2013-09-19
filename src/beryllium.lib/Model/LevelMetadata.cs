using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Nbt;



namespace beryllium.lib.Model {
   public class LevelMetadata {
      internal LevelMetadata() { }

      public int Version              { get; internal set; }
      public int IsInitialized        { get; internal set; }
      public string LevelName         { get; internal set; }
      public string GeneratorName     { get; internal set; }
      public int GeneratorVersion     { get; internal set; }
      public string GeneratorOptions  { get; internal set; }
      public long RandomSeed          { get; internal set; }
      public int MapFeatures          { get; internal set; }
      public long LastPlayed          { get; internal set; }
      public int AllowCommands        { get; internal set; }
      public int IsHardCore           { get; internal set; }
      public int GameType             { get; internal set; }
      public long Time                { get; internal set; }
      public long DayTime             { get; internal set; }
      public int SpawnX               { get; internal set; }
      public int SpawnY               { get; internal set; }
      public int SpawnZ               { get; internal set; }
      public int IsRaining            { get; internal set; }
      public int RainTime             { get; internal set; }
      public int IsThundering         { get; internal set; }
      public int ThunderTime          { get; internal set; }

      public IEnumerable<DimensionPointer> DimensionPointers {
         get {
            // TODO
            yield break;
         }
      }
   }
}
