﻿using System;

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
        { get; set; }

        public PointCard() { }
        public PointCard(int p, int c)
        {
            Points = p;
            PunchCards = c;
        }

        public void AddPoints(int point)
        {

        }

        public void RedeemPoints(int point)
        {

        }

        public void Punch()
        {

        }

        public override string ToString()
        {
            return "";
        }
    }
}
