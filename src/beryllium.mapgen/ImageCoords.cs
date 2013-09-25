using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beryllium.mapgen {
   [DebuggerDisplay("{DebugString}")]
   internal class ImageCoords {
      public virtual int X { get; private set; }
      public virtual int Y { get; private set; }

      public ImageCoords(int x, int y) {
         X = x;
         Y = y;
      }


      public Size GetExtentTo(ImageCoords farCorner) {
         return new Size(farCorner.X - this.X,
                         farCorner.Y - this.Y);
      }


      public Point ToPoint() {
         return new Point(X, Y);
      }

      public Size ToSize() {
         return new Size(X, Y);
      }

      public string DebugString { get { return string.Format("({0},{1})", X, Y); } }
   }



   //internal sealed class RelativeImageCoords : ImageCoords {
   //   public ImageCoords RelativeTo { get; private set; }
   //   public override int X { get { return RelativeTo.X + base.X; } }
   //   public override int Y { get { return RelativeTo.Y + base.Y; } }

   //   public RelativeImageCoords(int x, int y, ImageCoords relativeTo)
   //      : base(x, y) {
   //      RelativeTo = relativeTo;
   //   }
   //}

   //internal class ImageCoords {
   //   public int X, Y;
   //}

   //internal class ImageWindow {
   //   public ImageCoords Min, Max;

   //   public int Width {
   //      get { return Max.X - Min.X + 1; }
   //   }

   //   public int Height {
   //      get { return Max.Y - Max.Y + 1; }
   //   }


   //   //public ImageWindow InnerWindow(ImageWindow imageWindow) {
   //   //   return new InnerImageWindow();
   //   //}

   //   public Rectangle ToRectangle() {
   //      return new Rectangle(Min.X, Min.Y, Width, Height);
   //   }
   //}
}
