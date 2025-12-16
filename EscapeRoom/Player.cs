using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    // Simple player class with a health variable and an Inventory
    public class Player
    {
        #region Properties
        public List<Item> Inventory { get; set; } = new List<Item>();
        public bool GameOver { get; set; } = false;
        public bool Win { get; set; } = false;
        public int Health { get; set; }
        public Room Currentroom { get; set; }
        #endregion

        public Player(int health, Room currentroom, bool gameOver)
        {
            Health = health;
            Currentroom = currentroom;
            GameOver = gameOver;
        }

        public int LoseHealth()
        {
            Health -= 1;
            return Health;
        }



        public void AddToInventory(Item item)
        {
            Inventory.Add(item);
        }


    }
}
