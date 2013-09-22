using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.mapgen {
   internal class ImageCoords {
      public int X, Y;
   }

   internal class ImageWindow {
      public ImageCoords Min, Max;

      public int Width {
         get { return Max.X - Min.X + 1; }
      }

      public int Height {
         get { return Max.Y - Max.Y + 1; }
      }


      //public ImageWindow InnerWindow(ImageWindow imageWindow) {
      //   return new InnerImageWindow();
      //}

      public Rectangle ToRectangle() {
         return new Rectangle(Min.X, Min.Y, Width, Height);
      }
   }
}
