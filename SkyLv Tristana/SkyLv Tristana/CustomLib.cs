namespace SkyLv_Tristana
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
                return SkyLv_Tristana.Player;
            }
        }
        #endregion


        public static float EnemyMinionInMinionRange(Obj_AI_Minion Minion, float Range)
        {
            return ObjectManager.Get<Obj_AI_Minion>().Where(m => m.Team != ObjectManager.Player.Team && m.Distance(Minion) <= Range && !m.IsDead).Count();
        }

        public static float EnemyMinionInPlayerRange(float Range)
        {
            return ObjectManager.Get<Obj_AI_Minion>().Where(m => m.Team != ObjectManager.Player.Team && m.Distance(Player) <= Range && !m.IsDead).Count();
        }

        public static float EnemyMinionInHeroRange(Obj_AI_Hero Hero, float Range)
        {
            return ObjectManager.Get<Obj_AI_Minion>().Where(m => m.Team != ObjectManager.Player.Team && m.Distance(Hero) <= Range && !m.IsDead).Count();
        }
    }
}
