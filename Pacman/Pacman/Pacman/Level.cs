using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Level
    {
        byte[,] level;
        StreamReader reader;
        int idxLigne;
        string ligne;
        string[] tabLigne;

        public Level()
        {
            level = new byte[Grid.GRID_WIDTH, Grid.GRID_HEIGHT];
            reader = new StreamReader("Content/level.txt", Encoding.UTF8);
        }

        public void loadNextLevel()
        {
            idxLigne = 0;

            if (reader.ReadLine() == null)
            {
                reader.Close();
                reader = new StreamReader("level.txt", Encoding.UTF8);
                reader.ReadLine();
            }
            while (idxLigne < Grid.GRID_HEIGHT)
            {
                ligne = reader.ReadLine();

                tabLigne = ligne.Split(' ');
                for (int i = 0; i < Grid.GRID_WIDTH; i++)
                {
                    level[i, idxLigne] = Convert.ToByte(tabLigne[i]);
                }

                idxLigne++;
            }
        }

        public byte[,] getLevel()
        {
            return level;
        }
    }
}
