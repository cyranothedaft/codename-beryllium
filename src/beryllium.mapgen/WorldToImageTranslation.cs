using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beryllium.lib.Model;



namespace beryllium.mapgen {
   internal class WorldToImageTranslation<T> where T:WorldCoords, new() {
      private readonly Func<T, ImageCoords> _translateCoords;


      public WorldToImageTranslation(Func<T,ImageCoords> translateCoords) {
         _translateCoords = translateCoords;
      }


      public ImageCoords Translate(T worldCoords) {
         return _translateCoords(worldCoords);
      }


      public ImageWindow Translate(WorldWindow<T> worldExtents) {
         return new ImageWindow()
                   {
                      Min = _translateCoords(worldExtents.Min),
                      Max = _translateCoords(worldExtents.Max)
                   };
      }
   }
}
