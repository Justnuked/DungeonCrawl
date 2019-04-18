using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes
{
    public static class TextureLoader
    {

        public static Dictionary<String, T> LoadTextures<T>(this ContentManager content, string folder)
        {
            Dictionary<String, T> result = new Dictionary<string,T>();

            DirectoryInfo dir = new DirectoryInfo(content.RootDirectory + "/" + folder);

            if (!dir.Exists)
                throw new DirectoryNotFoundException();
            
            FileInfo[] files = dir.GetFiles("*.*");

            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);

                 result[key] = content.Load<T>(folder + "/" + key);
            }

            return result;
        }
    }
}
