namespace SkyLv_FountainTravelling
{

    using LeagueSharp;
    using LeagueSharp.Common;

    internal class SkyLv_FountainTravelling
    {

        public static Menu Menu;

        public static Obj_AI_Hero Player
        {
            get
            {
                return ObjectManager.Player;
            }
        }

        public SkyLv_FountainTravelling()
        {

            Menu = new Menu("SkyLv Fountain Travelling By LuNi", "SkyLv Fountain Travelling By LuNi", true);

            Menu.AddToMainMenu();

            new FountainMoves();
        }
    }
}