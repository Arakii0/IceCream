//==========================================================
// Student Number : S10257176H
// Student Name : Araki Yeo
// Partner Name : 
//==========================================================

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
        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
