namespace SkyLv_Jax
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
                return SkyLv_Jax.Player;
            }
        }
        #endregion


        public static float enemyChampionInPlayerRange(float Range)
        {
            return ObjectManager.Get<Obj_AI_Hero>().Where(target => !target.IsMe && target.Team != ObjectManager.Player.Team && target.Distance(Player) <= Range).Count();
        }

        public static float EnemyMinionInPlayerRange(float Range)
        {
            return ObjectManager.Get<Obj_AI_Minion>().Where(m => m.Team != ObjectManager.Player.Team && m.Distance(Player) <= Range && !m.IsDead).Count();
        }

        public static bool iSJaxEActive()
        {
            if (Player.HasBuff("JaxEvasion"))
            {
                return true;
            }
            else return false;
        }
    }
}
