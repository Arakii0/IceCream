using System;

namespace S10257176_PRG2Assignment
{
    class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;

        public string Type
        { get; set; }
        public bool Premium
        { get; set; }
        public int Quantity
        { get; set; }

        public Flavour() { }
        public Flavour(string t, bool p, int q)
        {
            Type = t;
            Premium = p;
            Quantity = q;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
