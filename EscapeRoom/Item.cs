using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EscapeRoom
{

    // The Item class acts as a puzzle / clue.
    // For example the item object would be a note or a book with a riddle for the code to a door.
    public class Item
    {
        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        #endregion

        public Item(string name, string description)
        {
            Name = name;
            Description = description;

        }

        public override string ToString()
        {
            return string.Format(Name);
        }
    }
}

