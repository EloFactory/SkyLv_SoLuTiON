namespace SkyLv_Fountain
{

    using LeagueSharp;
    using LeagueSharp.Common;

    internal class SkyLv_Fountain
    {

        public static Menu Menu;

        public static Obj_AI_Hero Player
        {
            get
            {
                return ObjectManager.Player;
            }
        }

        public SkyLv_Fountain()
        {

            Menu = new Menu("SkyLv Fountain Travelling By LuNi", "SkyLv Fountain Travelling By LuNi", true);

            Menu.AddToMainMenu();

            new FountainMoves();
        }
    }
}