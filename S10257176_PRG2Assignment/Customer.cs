//==========================================================
// Student Number : Benjamin Hwang
// Student Name : S10262171E
// Partner Name : Araki Yeo
//==========================================================

using System;

namespace S10257176_PRG2Assignment
{
    class Customer
    {
        private string name;
        private int memberId;
        private DateTime dob;
        private Order currentOrder;
        private List<Order> orderHistory;
        private PointCard rewards;
        private int orderCount;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int MemberId
        {
            get { return memberId; }
            set { memberId = value;  }
        }
        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }
        public Order CurrentOrder
        { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();

        public PointCard Rewards
        {
            get { return rewards; }
            set { rewards = value; }
        }

        public int OrderCount { get; set; } = 0;

        public Customer() { }

        public Customer(string name, int memberId, DateTime dob)
        {
            Name = name;
            MemberId = memberId;
            Dob = dob;
        }

        public Order MakeOrder()
        {
            return new Order(); 
        }


        public bool IsBirthday()
        {
            if (DateTime.Today.Day == Dob.Day && DateTime.Today.Month == Dob.Month) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Name: {Name} \tMemberId: {MemberId} \tDate of Birth: {Dob} \tRewards: {Rewards} ";
        }

    }
}
