﻿//==========================================================
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
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCards = punchCard;
        }

        public void AddPoints(int point)
        {
            Points += point;

            if (Tier == "Ordinary")
            {
                if (Points >= 100)
                {
                    Tier = "Gold";
                }
                else if (Points >= 50)
                {
                    Tier = "Silver";
                }
            }
            else if (Tier == "Silver")
            {
                if (Points >= 100)
                {
                    Tier = "Gold";
                }
            }
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
            return $"{Points,-10} {PunchCards,-12} {Tier}";
        }
    }
}
