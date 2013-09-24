using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.mapgen {
   internal class ImageWindow {
      public ImageCoords Location { get; private set; }
      public Size Size { get; private set; }

      public ImageWindow(int x, int y, int width, int height) {
         Location = new ImageCoords(x, y);
         Size = new Size(width, height);
      }

      public ImageWindow(ImageCoords location, Size size) {
         Location = location;
         Size = size;
      }

      public int Width { get { return Size.Width; } }
      public int Height { get { return Size.Height; } }


      public Rectangle ToRectangle() {
         return new Rectangle(Location.ToPoint(), Size);
      }
   }

   internal sealed class RelativeImageWindow : ImageWindow {
      public ImageWindow RelativeTo { get; private set; }

      public RelativeImageWindow(int x, int y, int width, int height, ImageWindow relativeTo)
         : base(x, y, width, height) {
         RelativeTo = relativeTo;
      }
   }
}
