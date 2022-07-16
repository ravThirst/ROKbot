using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAF
{
    public enum Decision
    {
        Call,Fold,Nothing
    }
    public static class AI
    {
        public static HashSet<string> str = new HashSet<string>
        {"AEIGH", "ANINE","ATEN", "AJ","AQ", "AK","AA", "KTEN",
        "KJ", "KQ","KK", "AKS","QJ", "QQ","KQS", "AQS","JJ", "QJS",
        "KJS", "AJS","TENTEN", "JTENS","QTENS", "KTENS","ATENS", "NINENINE","KNINES", "ANINE",
        "ANINES", "EIGHTEIGHT","AEIGHTS", "SEVENSEVEN","ASEVENS", "SIXSIX","ASIXS", "FIVEFIVE",
        "AFIVES", "AFOURS","ATHREES", "ATWOS", "FOURFOUR","AFIVE", "ASIX", "ASEVEN",
        "EIGHTSEVENS", "NINEEIGHTS","TENNINES", "JNINES", "QNINES", "AEIGHT", "AFOUR", "ATHREE", "ATWO",
        "THREETHREE", "TWOTWO", "FIVEFOURS", "SIXFIVES", "SEVENSIXS"};

        public static Decision AIDecision(Desk desk)
        {
            string s = String.Empty;
            if (desk.FirstCard != Card.NA && desk.SecondCard != Card.NA)
            {
                var t = desk.SameSuit ? "S" : "";
                s += desk.FirstCard.ToString() + desk.SecondCard.ToString() + t;
                var str = Strategy(s);
                desk.Decision = str;
                return str;
            }
            else
            {
                desk.Decision = Decision.Nothing;
                return Decision.Nothing;
            }

        }

        public static Decision Strategy(string s)
        {
            if (str.Contains(s))
                return Decision.Call;
            else
                return Decision.Fold;
        }
    }
}
