namespace SkyLv_FountainTravelling
{
    using System;

    using LeagueSharp;
    using LeagueSharp.Common;

    internal class Initialiser
    {

        private static void Game_OnGameLoad(EventArgs args)
        {

            new SkyLv_FountainTravelling();
            
        }

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }
    }
}