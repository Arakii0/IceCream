using System;

namespace S10257176_PRG2Assignment
{
    class Order
    {
        private int id;
        private DateTime timeRecieved;
        private DateTime? timeFulfilled;
        private List<IceCream> iceCreamList;

        public int Id
        { get; set; }
        public DateTime TimeRecieved
        { get; set; }
        public DateTime? TimeFulfilled
        { get; set; }
        public List<IceCream> IceCreamList { get; set; } = new List<IceCream>();


        public Order() { }
        public Order(int id, DateTime tr)
        {
            Id = id;
            TimeRecieved = tr;
        }

        public void ModifyIceCream(int id)
        {

        }

        public void AddIceCream(IceCream iceCream)
        {

        }

        public void DeleteIceCream(int id)
        {

        }

        public double CalculateTotal()
        {
            return 0.00;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
