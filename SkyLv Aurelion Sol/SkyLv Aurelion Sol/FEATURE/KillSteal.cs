namespace SkyLv_AurelionSol
{
    using System;
    using System.Linq;

    using LeagueSharp;
    using LeagueSharp.Common;


    internal class KillSteal
    {
        #region #GET
        private static Obj_AI_Hero Player
        {
            get
            {
                return SkyLv_AurelionSol.Player;
            }
        }

        private static Spell Q
        {
            get
            {
                return SkyLv_AurelionSol.Q;
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




        static KillSteal()
        {
            //Menu
            SkyLv_AurelionSol.Menu.SubMenu("Combo").AddSubMenu(new Menu("KS Mode", "KS Mode"));
            SkyLv_AurelionSol.Menu.SubMenu("Combo").SubMenu("KS Mode").AddItem(new MenuItem("AurelionSol.UseIgniteKS", "KS With Ignite").SetValue(true));
            SkyLv_AurelionSol.Menu.SubMenu("Combo").SubMenu("KS Mode").AddItem(new MenuItem("AurelionSol.UseQKS", "KS With Q").SetValue(true));
            SkyLv_AurelionSol.Menu.SubMenu("Combo").SubMenu("KS Mode").AddItem(new MenuItem("AurelionSol.UseWKS", "KS With W").SetValue(true));
            SkyLv_AurelionSol.Menu.SubMenu("Combo").SubMenu("KS Mode").AddItem(new MenuItem("AurelionSol.UseRKS", "KS With R").SetValue(true));
            SkyLv_AurelionSol.Menu.SubMenu("Combo").SubMenu("KS Mode").AddItem(new MenuItem("AurelionSol.PacketCastKS", "PacketCast KS").SetValue(false));

            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            KS();
        }

        public static void KS()
        {
            var PacketCast = SkyLv_AurelionSol.Menu.Item("AurelionSol.PacketCastKS").GetValue<bool>();
            var useIgniteKS = SkyLv_AurelionSol.Menu.Item("AurelionSol.UseIgniteKS").GetValue<bool>();
            var useQKS = SkyLv_AurelionSol.Menu.Item("AurelionSol.UseQKS").GetValue<bool>();
            var useWKS = SkyLv_AurelionSol.Menu.Item("AurelionSol.UseWKS").GetValue<bool>();
            var useRKS = SkyLv_AurelionSol.Menu.Item("AurelionSol.UseRKS").GetValue<bool>();

            foreach (var target in ObjectManager.Get<Obj_AI_Hero>().Where(target => !target.IsMe && target.Team != ObjectManager.Player.Team && !target.IsZombie && (SkyLv_AurelionSol.Ignite.Slot != SpellSlot.Unknown || !target.HasBuff("summonerdot"))))
            {
                if (!target.IsDead)
                {
                    if (useQKS && Q.IsReady() && target.Health < MathsLib.QDamage(target))
                    {
                        var prediction = Q.GetPrediction(target);
                        if (prediction.Hitchance >= HitChance.High)
                        {
                            Q.Cast(prediction.CastPosition, PacketCast);
                        }
                    }

                    if (useWKS && W1.IsReady() && target.Health < W1.GetDamage(target))
                    {
                        if (Player.Distance(target) > W1.Range - 20 && Player.Distance(target) < W1.Range + 20 && MathsLib.isWInLongRangeMode())
                        {
                            W2.Cast(PacketCast);
                        }

                        if (Player.Distance(target) > W2.Range - 20 && Player.Distance(target) < W2.Range + 20 && !MathsLib.isWInLongRangeMode())
                        {
                            W1.Cast(PacketCast);
                        }
                    }

                    if (useRKS && R.IsReady() && target.Health < MathsLib.RDamage(target))
                    {
                        var prediction = R.GetPrediction(target);
                        if (prediction.Hitchance == HitChance.VeryHigh)
                        {
                            R.Cast(prediction.CastPosition, PacketCast);
                        }
                    }

                    if (useIgniteKS && SkyLv_AurelionSol.Ignite.Slot != SpellSlot.Unknown && Player.GetSummonerSpellDamage(target, Damage.SummonerSpell.Ignite) > target.Health && Player.Distance(target) <= SkyLv_AurelionSol.Ignite.Range)
                    {
                        Player.Spellbook.CastSpell(SkyLv_AurelionSol.Ignite.Slot, target);
                    }
                }
            }
        }
    }
}
