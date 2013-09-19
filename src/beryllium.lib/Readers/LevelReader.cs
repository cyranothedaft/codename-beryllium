using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Exceptions;
using beryllium.lib.Model;
using beryllium.lib.Nbt;



namespace beryllium.lib.Readers {
   internal sealed class LevelReader {
      public const string LevelFileName = "level.dat";

      internal const string TagName_Data = "Data";
      internal const string TagName_Version = "version";
      internal const string TagName_Initialized = "initialized";
      internal const string TagName_LevelName = "LevelName";
      internal const string TagName_GeneratorName = "generatorName";
      internal const string TagName_GeneratorVersion = "generatorVersion";
      internal const string TagName_GeneratorOptions = "generatorOptions";
      internal const string TagName_RandomSeed = "RandomSeed";
      internal const string TagName_MapFeatures = "MapFeatures";
      internal const string TagName_LastPlayed = "LastPlayed";
      internal const string TagName_AllowCommands = "allowCommands";
      internal const string TagName_HardCore = "hardcore";
      internal const string TagName_GameType = "GameType";
      internal const string TagName_Time = "Time";
      internal const string TagName_DayTime = "DayTime";
      internal const string TagName_SpawnX = "SpawnX";
      internal const string TagName_SpawnY = "SpawnY";
      internal const string TagName_SpawnZ = "SpawnZ";
      internal const string TagName_Raining = "raining";
      internal const string TagName_RainTime = "rainTime";
      internal const string TagName_Thundering = "thundering";
      internal const string TagName_ThunderTime = "thunderTime";

      /// <summary>
      /// 
      /// </summary>
      /// <param name="levelFilePath">Refers either to a level.dat file or a world level directory that contains a level.dat file.</param>
      public static LevelMetadata ReadMetadata(string levelFilePath) {
         // TODO ? read session.lock file and issue suitable warning

         if ( !string.Equals(Path.GetFileName(levelFilePath), LevelFileName, StringComparison.OrdinalIgnoreCase) )
            levelFilePath = Path.Combine(levelFilePath, LevelFileName);

         if ( !File.Exists(levelFilePath) )
            throw new FileNotFoundException(string.Concat(LevelFileName, " not found at specified path: ", levelFilePath), levelFilePath);

         LevelMetadata levelMetadata;

         using ( FileStream fileStream = new FileStream(levelFilePath, FileMode.Open, FileAccess.Read) )
         using ( GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress, false) )
         using ( BinaryReader binReader = new BinaryReader(gzipStream) ) {
            NbtReader rdr = new NbtReader(binReader);
            // read root tag (should be single compound tag)
            NbtTag rootTag = rdr.ReadNextTag();

            levelMetadata = getLevelMetadata(rootTag);
         }

         return levelMetadata;
      }


      private static LevelMetadata getLevelMetadata(NbtTag rootTag) {
         if ( rootTag == null ) throw new ArgumentNullException("rootTag");

         var rootPayload = rootTag.Payload as NbtTagPayload_Compound;

         if ( rootPayload == null ) throw new InvalidLevelStructureException(string.Format("Root tag payload type must be Compound, but is {0}.", rootTag.Payload.GetType().Name));

         // get Data tag
         var dataTagPayload = rootPayload.GetCompoundTagPayload(TagName_Data);
         if ( dataTagPayload == null ) throw new InvalidLevelStructureException(string.Format("{0} tag payload type must be Compound, but is {1}.", TagName_Data, rootTag.Payload.GetType().Name));
         // TODO: ? handle other top-level tags (Forge, FML, ...)

         LevelMetadata levelMetadata = getLevelTags(dataTagPayload);
         return levelMetadata;
      }


      private static LevelMetadata getLevelTags(NbtTagPayload_Compound dataTagPayload) {
         LevelMetadata levelMetadata = new LevelMetadata()
            {
               Version = dataTagPayload.GetScalarTagValue<int>(TagName_Version),
               IsInitialized = dataTagPayload.GetScalarTagValue<sbyte>(TagName_Initialized),
               LevelName = dataTagPayload.GetScalarTagValue<string>(TagName_LevelName),
               GeneratorName = dataTagPayload.GetScalarTagValue<string>(TagName_GeneratorName),
               GeneratorVersion = dataTagPayload.GetScalarTagValue<int>(TagName_GeneratorVersion),
               GeneratorOptions = dataTagPayload.GetScalarTagValue<string>(TagName_GeneratorOptions),
               RandomSeed = dataTagPayload.GetScalarTagValue<long>(TagName_RandomSeed),
               MapFeatures = dataTagPayload.GetScalarTagValue<sbyte>(TagName_MapFeatures),
               LastPlayed = dataTagPayload.GetScalarTagValue<long>(TagName_LastPlayed),
               AllowCommands = dataTagPayload.GetScalarTagValue<sbyte>(TagName_AllowCommands),
               IsHardCore = dataTagPayload.GetScalarTagValue<sbyte>(TagName_HardCore),
               GameType = dataTagPayload.GetScalarTagValue<int>(TagName_GameType),
               Time = dataTagPayload.GetScalarTagValue<long>(TagName_Time),
               DayTime = dataTagPayload.GetScalarTagValue<long>(TagName_DayTime),
               SpawnX = dataTagPayload.GetScalarTagValue<int>(TagName_SpawnX),
               SpawnY = dataTagPayload.GetScalarTagValue<int>(TagName_SpawnY),
               SpawnZ = dataTagPayload.GetScalarTagValue<int>(TagName_SpawnZ),
               IsRaining = dataTagPayload.GetScalarTagValue<sbyte>(TagName_Raining),
               RainTime = dataTagPayload.GetScalarTagValue<int>(TagName_RainTime),
               IsThundering = dataTagPayload.GetScalarTagValue<sbyte>(TagName_Thundering),
               ThunderTime = dataTagPayload.GetScalarTagValue<int>(TagName_ThunderTime),
               //Player = dataTagPayload.GetCompoundTagPayload(TagName_Player),
               //GameRules = dataTagPayload.GetCompoundTagPayload(TagName_GameRules),
            };
         return levelMetadata;
      }
   }
}
