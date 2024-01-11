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

        public string Name
        { get; set; }
        public int MemberId
        { get; set; }
        public DateTime Dob
        { get; set; }
        public Order CurrentOrder
        { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public PointCard Rewards
        { get; set; }
    
    }
}
