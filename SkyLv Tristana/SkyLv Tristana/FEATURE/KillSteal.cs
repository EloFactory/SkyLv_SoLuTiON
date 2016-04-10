namespace SkyLv_Tristana
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
                return SkyLv_Tristana.Player;
            }
        }

        private static Spell E
        {
            get
            {
                return SkyLv_Tristana.E;
            }
        }

        private static Spell R
        {
            get
            {
                return SkyLv_Tristana.R;
            }
        }
        #endregion




        static KillSteal()
        {
            //Menu
            SkyLv_Tristana.Menu.SubMenu("Combo").AddSubMenu(new Menu("KS Mode", "KS Mode"));
            SkyLv_Tristana.Menu.SubMenu("Combo").SubMenu("KS Mode").AddItem(new MenuItem("Tristana.UseIgniteKS", "KS With Ignite").SetValue(true));
            SkyLv_Tristana.Menu.SubMenu("Combo").SubMenu("KS Mode").AddItem(new MenuItem("Tristana.UseEKS", "KS With E").SetValue(true));
            SkyLv_Tristana.Menu.SubMenu("Combo").SubMenu("KS Mode").AddItem(new MenuItem("Tristana.UseRKS", "KS With R").SetValue(false));
            SkyLv_Tristana.Menu.SubMenu("Combo").SubMenu("KS Mode").AddItem(new MenuItem("Tristana.PacketCastKS", "PacketCast KS").SetValue(false));

            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            KS();
        }

        #region KillSteal
        public static void KS()
        {
            var PacketCast = SkyLv_Tristana.Menu.Item("Tristana.PacketCastKS").GetValue<bool>();
            var useIgniteKS = SkyLv_Tristana.Menu.Item("Tristana.UseIgniteKS").GetValue<bool>();
            var useEKS = SkyLv_Tristana.Menu.Item("Tristana.UseEKS").GetValue<bool>();
            var useRKS = SkyLv_Tristana.Menu.Item("Tristana.UseRKS").GetValue<bool>();

            foreach (var target in ObjectManager.Get<Obj_AI_Hero>().Where(target => !target.IsMe && target.Team != ObjectManager.Player.Team && target.Distance(Player) < 1200 && !target.IsZombie && (SkyLv_Tristana.Ignite.Slot != SpellSlot.Unknown || !target.HasBuff("summonerdot"))))
            {

                if (useEKS && E.GetDamage(target) > target.Health)
                {
                    E.Cast(target, PacketCast);
                }

                if (useRKS && (R.GetDamage(target) > target.Health || (SkyLv_Tristana.ERKSState == true && target.HasBuff("TristanaECharge"))))
                {
                    R.CastIfHitchanceEquals(target, HitChance.VeryHigh, PacketCast);
                    SkyLv_Tristana.ERKSState = false;
                }

                if (useEKS && useRKS && E.GetDamage(target) + R.GetDamage(target) > target.Health && E.IsReady() && R.IsReady() && Player.Mana > E.ManaCost + R.ManaCost)
                {
                    E.Cast(target, PacketCast);
                    SkyLv_Tristana.ERKSState = true;
                }

                if (useIgniteKS && SkyLv_Tristana.Ignite.Slot != SpellSlot.Unknown && Player.GetSummonerSpellDamage(target, Damage.SummonerSpell.Ignite) > target.Health)
                {
                    Player.Spellbook.CastSpell(SkyLv_Tristana.Ignite.Slot, target);
                }

            }
        }
        #endregion
    }
}
