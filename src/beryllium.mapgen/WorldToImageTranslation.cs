using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;



namespace beryllium.mapgen {
   internal class WorldToImageTranslation {
      private readonly WorldWindow _worldExtents_block;


      public WorldToImageTranslation(WorldWindow worldExtents_block) {
         _worldExtents_block = worldExtents_block;
      }


      public ImageCoords Translate(WorldCoords worldCoords) {
         return translateCoords(worldCoords);
      }


      public ImageWindow Translate(WorldWindow worldWindow) {
         ImageCoords location = translateCoords(worldWindow.Location),
                     extent = translateExtents(worldWindow.Extent);
         return new ImageWindow(location, extent.ToSize());
      }


      private ImageCoords translateCoords(WorldCoords worldCoords) {
         // TODO: account for: z-coordinate is mirrored north-to-south
         return new ImageCoords(
            worldCoords.Z - _worldExtents_block.Location.Z,
            worldCoords.X - _worldExtents_block.Location.X
            );
      }

      private ImageCoords translateExtents(WorldCoords worldCoords) {
         return new ImageCoords(
            worldCoords.Z,
            worldCoords.X
            );
      }
   }
}
