namespace SkyLv_Fountain
{
    using System;

    using LeagueSharp;
    using LeagueSharp.Common;

    internal class Initialiser
    {

        private static void Game_OnGameLoad(EventArgs args)
        {

            new SkyLv_Fountain();
            
        }

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }
    }
}