using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib;
using beryllium.lib.Model;
using Region = beryllium.lib.Model.Region;



namespace beryllium.mapgen {
   public sealed class HeightMapGenerator : IWorldProcessor {
      private readonly string _outputDir;
      private readonly string _filenamePrefix;

      //private int _width, _height;
      //private int _minRegionX,
      //            _minRegionZ,
      //            _maxRegionX,
      //            _maxRegionZ;
      private ImageWindow _imageExtent;

      private WorldWindow _regionExtents;

      //private WorldCoords _minWorldCoords,
      //                    _maxWorldCoords;

      private WorldToImageTranslation _translation;

      private Bitmap _bmpHeight;
      private BitmapData _bmpHeightRegionData;


      public HeightMapGenerator(string outputDir, string filenamePrefix = "") {
         _outputDir = outputDir;
         _filenamePrefix = filenamePrefix;
      }


      public void ProcessLevelDirectory(LevelDirectoryMetadata levelDirectoryMetadata) {
         //
      }


      public void ProcessLevelMetadata(LevelMetadata levelMetadata) {
         //
      }



      public void ProcessDimensionMetadata(DimensionMetadata dimension) {
         _bmpHeight = null;
         if ( !dimension.HasRegions ) return;

         var regionCoords = dimension.RegionPointers.Select(r => r.RegionCoords);
         // scan map data to determine overall extent
         // (used to determine map image dimensions and map-to-image block translation)
         _regionExtents = WorldWindow.ExtentFromCoordList(regionCoords);

         //_minWorldCoords = WorldCoords.FromRegion(_minRegionX, _minRegionZ);
         //_maxWorldCoords = WorldCoords.FromRegion(_maxRegionX + 1, _maxRegionZ + 1);

         WorldWindow blockExtents = _regionExtents.ConvertTo(WorldCoordUnit.Block);

         _translation = new WorldToImageTranslation(
            w => new ImageCoords(
                    blockExtents.Supremum.Z - w.Z,
                    blockExtents.Supremum.X - w.X));
            
         _imageExtent = _translation.Translate(blockExtents);

         //_width = _maxWorldCoords.X - _minWorldCoords.X + 1;
         //_height = _maxWorldCoords.Z - _minWorldCoords.Z + 1;

         // prepare image layers
         _bmpHeight = prepareNewBitmap(_imageExtent, PixelFormat.Format24bppRgb);
         //_bmpHeight = prepareNewBitmap(_width, _height, PixelFormat.Format8bppIndexed);
      }


      private static Bitmap prepareNewBitmap(ImageWindow imageExtent, PixelFormat pixelFormat) {
         Bitmap bitmap = new Bitmap(imageExtent.Width, imageExtent.Height, pixelFormat);
         //Graphics.FromImage(bitmap).Clear(Color.Bisque);
         bitmap.MakeTransparent();
         return bitmap;
      }


      public void ProcessRegionHeader(Region region) {
         // determine image rectangle corresponding to this region

         WorldWindow regionWindow = new WorldWindow(region.RegionPointer.RegionCoords, 1, 1)
            .ConvertTo(WorldCoordUnit.Block);

         ImageWindow regionImg = _translation.Translate(regionWindow);

         // lock region in bitmap for direct-memory pixel access
         _bmpHeightRegionData = _bmpHeight.LockBits(regionImg.ToRectangle(), ImageLockMode.WriteOnly, _bmpHeight.PixelFormat);
      }


      public void ProcessChunk(Chunk chunk) {
         if ( chunk == null || !chunk.HasData ) return;
         setHeightMapPixels(chunk, _bmpHeightRegionData);
      }


      private unsafe void setHeightMapPixels(Chunk chunk, BitmapData bmpHeightData) {
         byte* scan0 = ( byte* )bmpHeightData.Scan0;

         WorldCoords chunkCoords = chunk.ChunkCoords.ConvertTo(WorldCoordUnit.Block);
         ImageCoords chunkImageCoords = _translation.Translate(chunkCoords);

         // iterate through all 16*16 blocks in the chunk's height map
         for ( int x = 0; x < 16; ++x )
         for ( int z = 0; z < 16; ++z ) {
            int heightMapIndex = ( z * 16 ) + x;
            int height = chunk.HeightValues[heightMapIndex];

            // skip if height is unset
            if ( height == -1 ) continue;

            WorldCoords blockCoords = chunkCoords.Offset(x, z);
            ImageCoords blockImageCoords = _translation.Translate(blockCoords);

            byte scaledHeightValue = ( byte )height;

            int rectX = blockImageCoords.X - chunkImageCoords.X,
                rectY = blockImageCoords.Y - chunkImageCoords.Y;
            int pixelByteIndex = rectY * _bmpHeightRegionData.Stride + rectX * 3;
            scan0[pixelByteIndex] = scaledHeightValue;
         }
      }


      public void ProcessRegionEnd(Region region) {
         if ( _bmpHeight != null && _bmpHeightRegionData != null )
            _bmpHeight.UnlockBits(_bmpHeightRegionData);
      }


      public void ProcessDimensionEnd(DimensionMetadata dimension) {
         if ( _bmpHeight != null ) {
            // write image file for this dimension
            string filename = string.Concat(_filenamePrefix, dimension.Name, ".height.png");
            string filePath = Path.Combine(_outputDir, filename);
            _bmpHeight.Save(filePath, ImageFormat.Png);
         }
      }


      public void ProcessLevelEnd(LevelMetadata levelMetadata) {
         //
      }


      //private void toImageCoords(WorldCoords worldCoords, out int imgX, out int imgZ) {
      //   // TODO: account for: z-coordinate is mirrored north-to-south
      //   imgX = worldCoords.X - _minWorldCoords.X;
      //   imgZ = worldCoords.Z - _minWorldCoords.Z;
      //}
   }
}
