using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GifSplitter
{
   
    class Program
    {
        
        static void Main(string[] args)
        {
            Image img = Image.FromFile(@"C:\Users\rachel.meyer\Desktop\Sprites\charmander.gif");

            List<Image> frames = new List<Image>();
            int frameNo = img.GetFrameCount(FrameDimension.Time);

            FileStream fs;

            for(int x = 0; x < frameNo; x++)
            {
                img.SelectActiveFrame(FrameDimension.Time, x);
                frames.Add(new Bitmap(img));
              
            }

            int count = 0;
            foreach(Image i in frames)
            {
                
                i.Save(@"C:\Users\rachel.meyer\Desktop\" + count + ".bmp");
                count++;
            }

        }
    }
}
