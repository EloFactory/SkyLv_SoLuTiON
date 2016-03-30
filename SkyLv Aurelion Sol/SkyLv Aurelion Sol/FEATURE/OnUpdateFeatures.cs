namespace SkyLv_AurelionSol
{
    using System;
    using System.Linq;

    using LeagueSharp;
    using LeagueSharp.Common;


    internal class OnUpdateFeatures
    {
        #region #GET
        private static Obj_AI_Hero Player
        {
            get
            {
                return SkyLv_AurelionSol.Player;
            }
        }

        private static Spell W1
        {
            get
            {
                return SkyLv_AurelionSol.W1;
            }
        }

        private static Spell W2
        {
            get
            {
                return SkyLv_AurelionSol.W2;
            }
        }

        private static Spell R
        {
            get
            {
                return SkyLv_AurelionSol.R;
            }
        }
        #endregion

        static OnUpdateFeatures()
        {
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            AutoR();
            AutoWManager();
        }


        public static void AutoWManager()
        {
            var target = TargetSelector.GetTarget(W2.Range + 50, TargetSelector.DamageType.Magical);
            var PacketCast = SkyLv_AurelionSol.Menu.Item("AurelionSol.UsePacketCastCombo").GetValue<bool>();
            var AutoWManager = SkyLv_AurelionSol.Menu.Item("AurelionSol.AutoManageW").GetValue<bool>();
            if (AutoWManager)
            {
                if (MathsLib.enemyChampionInRange(600 + 300) == 0 && MathsLib.isWInLongRangeMode())
                {
                    W2.Cast(PacketCast);
                }
            }
        }

        public static void AutoR()
        {
            var useAutoR = SkyLv_AurelionSol.Menu.Item("AurelionSol.AutoR").GetValue<bool>();
            var MinimumEnemyHitAutoR = SkyLv_AurelionSol.Menu.Item("AurelionSol.MinimumEnemyHitAutoR").GetValue<Slider>().Value;
            var PacketCast = SkyLv_AurelionSol.Menu.Item("AurelionSol.UsePacketCastCombo").GetValue<bool>();

            if (useAutoR)
            {
                float RRange = 1420;
                float RWidth = 120;
                foreach (var enemy in HeroManager.Enemies)
                {
                    var startPos = enemy.ServerPosition;
                    var endPos = Player.ServerPosition.Extend(startPos, Player.Distance(enemy) + RRange);
                    var rectangle = new Geometry.Polygon.Rectangle(startPos, endPos, RWidth);

                    if (HeroManager.Enemies.Count(x => rectangle.IsInside(x)) >= MinimumEnemyHitAutoR)
                    {
                        R.Cast(enemy, PacketCast);
                    }
                }
            }


        }

    }
}
