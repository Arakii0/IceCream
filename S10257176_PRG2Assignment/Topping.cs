//==========================================================
// Student Number : S10257176H 
// Student Name : Araki Yeo
// Partner Name : Benjamin Hwang
//==========================================================

using System;

namespace S10257176_PRG2Assignment
{
    class Topping
    {
        private string type;
        public string Type
        { get; set; }

        public Topping() { }
        public Topping(string t)
        {
            Type = t;
        }

        public override string ToString()
        {
            return $"Topping Type : {Type}";
        }
    }
}
