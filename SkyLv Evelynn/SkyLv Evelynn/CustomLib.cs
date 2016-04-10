namespace SkyLv_Evelynn
{
    using System.Linq;

    using LeagueSharp;
    using LeagueSharp.Common;

    public static class CustomLib
    {
        #region #GET
        private static Obj_AI_Hero Player
        {
            get
            {
                return SkyLv_Evelynn.Player;
            }
        }
        #endregion


        public static float enemyChampionInPlayerRange(float Range)
        {

            return ObjectManager.Get<Obj_AI_Hero>().Where(target => !target.IsMe && target.Team != ObjectManager.Player.Team && target.Distance(Player) <= Range).Count();

        }
    }
}
