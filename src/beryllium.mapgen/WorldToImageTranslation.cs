using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;



namespace beryllium.mapgen {
   internal class WorldToImageTranslation {
      private readonly Func<WorldCoords, ImageCoords> _translateCoords;


      public WorldToImageTranslation(Func<WorldCoords,ImageCoords> translateCoords) {
         _translateCoords = translateCoords;
      }


      public ImageCoords Translate(WorldCoords worldCoords) {
         return _translateCoords(worldCoords);
      }


      public ImageWindow Translate(WorldWindow worldWindow) {
         ImageCoords location = _translateCoords(worldWindow.Location),
                     extent = _translateCoords(worldWindow.Extent);
         return new ImageWindow(location, extent.ToSize());
      }
   }
}
