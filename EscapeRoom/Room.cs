using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    public class Room
    {
        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public string Inspect { get; set; }
        public bool Picked { get; set; } = false;
        public bool PuzzleSovled { get; set; } = false;
        public List<Item> Items { get; set; }
        #endregion
        public Room(string name, string description, string inspect)
        {
            Name = name;
            Description = description;
            Items = new List<Item>();
            Inspect = inspect;
        }
    }
}
