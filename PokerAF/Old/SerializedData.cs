using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace PokerAF
{
    public enum Card
    {
        TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, J, Q, K, A, NA
    }

    public enum Suit
    {
        DIAMONDS, CLUBS, HEARTS, SPADES
    }

    [DataContract]
    internal class SerializedData
    {
        [DataMember]
        public Dictionary<Rectangle, Color> Win { get; set; }

        [DataMember]
        public Dictionary<Rectangle, Color> Lose { get; set; }
    }
}