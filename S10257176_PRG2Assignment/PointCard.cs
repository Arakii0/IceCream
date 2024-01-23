//==========================================================
// Student Number : S10257176H
// Student Name : Araki Yeo
// Partner Name : Benjamin Hwang
//==========================================================

using System;

namespace S10257176_PRG2Assignment
{
    class PointCard
    {
        private int points;
        private int punchCard;
        private string tier;

        public int Points
        { get; set; }
        public int PunchCards
        { get; set; }
        public string Tier
        { get; set; } = "Ordinary";

        public PointCard() { }
        public PointCard(int p, int c)
        {
            Points = p;
            PunchCards = c;
        }

        public void AddPoints(int point)
        {
            Points += point;

            if(Points >= 100 && Tier != "Gold")
                Tier = "Gold";
            if (Points >= 50 && Tier != "Silver")
                Tier = "Silver";
        }

        public void RedeemPoints(int point)
        {
            Points -= point;
        }

        public void Punch()
        {
            PunchCards++;
        }

        public override string ToString()
        {
            return $"Points: {Points} \tPunchCards: {PunchCards} \tTier: {Tier}";
        }
    }
}
