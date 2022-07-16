using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAF
{
    public class Desk
    {
        public IntPtr Handle { get; set; }
        public Card FirstCard { get; set; }
        public Card SecondCard { get; set; }
        public bool SameSuit { get; set; }
        public Decision Decision { get; set; }

        public Desk(IntPtr Pointer)
        {
            this.Handle = Pointer;
            Decision = Decision.Nothing;
            FirstCard = Card.NA;
            SecondCard = Card.NA;
        }
    }
}