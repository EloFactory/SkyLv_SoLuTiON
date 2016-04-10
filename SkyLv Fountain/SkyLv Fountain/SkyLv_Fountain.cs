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

            Menu = new Menu("SkyLv Fountain By LuNi", "SkyLv Fountain By LuNi", true);

            Menu.AddToMainMenu();

            new FountainMoves();
        }
    }
}