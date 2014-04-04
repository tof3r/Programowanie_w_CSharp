using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace ImageResizer
{
    class Program
    {
        static int fileCounter = 1;

        public static int[] getDimens(string arg)
        {
            int[] dimens = new int[2];
            string tmp = arg.Substring(5, arg.Length - 5);
            int separator = tmp.IndexOf("x");
            string res_x = tmp.Substring(0, separator);
            string res_y = tmp.Substring(separator + 1);
            int resolutionX = Convert.ToInt32(res_x);
            int resolutionY = Convert.ToInt32(res_y);
            dimens[0] = resolutionX;
            dimens[1] = resolutionY;
            return dimens;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public static void getFilesAndResize(int[] dimens, string input, string output)
        {
            if (input == null && output == null)
            {
                input = Directory.GetCurrentDirectory();
                Directory.CreateDirectory(input+@"\output");
            }
            else if (output == null || output == "null")
            {
                input = input.Substring(10, input.Length - 10);
                Directory.CreateDirectory(input + @"\output");
            }
            else if (input == null || input == "null")
            {
                input = Directory.GetCurrentDirectory();
                output = output.Substring(11, output.Length - 11);
            }
            else 
            {
                input = input.Substring(10, input.Length - 10);
                output = output.Substring(11, output.Length - 11);
            }


            string[] paths = null;
            List<string> filesToResize = new List<string>();
            int fileExistedCounter = 0;

            paths = Directory.GetFiles(input);
            foreach (string file in paths)
            {
                if (file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".bmp"))
                    filesToResize.Add(file);
            }

            foreach (string resizer in filesToResize)
            {
                if (fileCounter == 1)
                    fileExistedCounter = 0;
                else if (fileCounter > 1 && fileCounter < 10)
                    fileExistedCounter = 1;
                else if (fileCounter >= 10)
                    fileExistedCounter = 1;

                Image tmp = Image.FromFile(resizer);
                Image img = resizeImage(tmp, new Size(dimens[0], dimens[1]));

                string mainPath = input+@"\";
                string fileName = Path.GetFileNameWithoutExtension(resizer.Substring(0, resizer.Length - fileExistedCounter));
                string extension = resizer.Substring(resizer.LastIndexOf("."));

                if (File.Exists(resizer))
                {
                    if(output != null)
                        img.Save(output + @"\" +fileName+ fileCounter + extension);
                    else
                        img.Save(mainPath +@"output\"+ fileName +fileCounter + extension);
                }
                if(resizer == filesToResize[filesToResize.Count-1])
                    fileCounter++;
            }
        }

        static void Main(string[] args)
        {
            int[] imageSize = new int[2];

            switch (args.Length)
            {
                case 0:
                    Console.WriteLine("\nProgram parameters: -res=widthxheight <-inputdir=path> <-outputdir=path>");
                    Console.WriteLine("Parameters inside <> brackets are optional.\n");
                    break;
                case 1:
                    imageSize = getDimens(args[0]);
                    getFilesAndResize(imageSize, null, null);
                    break;
                case 2:
                    imageSize = getDimens(args[0]);
                    getFilesAndResize(imageSize, args[1], null);
                    break;
                case 3:
                    imageSize = getDimens(args[0]);
                    if(args[1] == "-inputdir=null" || args[1] == ",,")
                        getFilesAndResize(imageSize, null, args[2]);
                    else
                        getFilesAndResize(imageSize, args[1], args[2]);
                    break;
            }
        }

    }
}
