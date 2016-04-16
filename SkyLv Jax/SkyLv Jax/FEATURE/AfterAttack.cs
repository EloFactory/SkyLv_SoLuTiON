namespace SkyLv_Jax
{
    using LeagueSharp;
    using LeagueSharp.Common;


    internal class AfterAttack
    {
        #region #GET
        private static Obj_AI_Hero Player
        {
            get
            {
                return SkyLv_Jax.Player;
            }
        }

        private static Spell W
        {
            get
            {
                return SkyLv_Jax.W;
            }
        }
        #endregion

        static AfterAttack()
        {
            //Menu
            SkyLv_Jax.Menu.SubMenu("Combo").AddItem(new MenuItem("Jax.AfterAttackModeCombo", "Cancel W With AA In Combo").SetValue(true));
            SkyLv_Jax.Menu.SubMenu("Harass").AddItem(new MenuItem("Jax.AfterAttackModeHarass", "Cancel W With AA In Harass").SetValue(true));

            Orbwalking.AfterAttack += Orbwalking_AfterAttack;
        }

        public static void Orbwalking_AfterAttack(AttackableUnit unit, AttackableUnit target)
        {
            var tb = target as Obj_AI_Hero;

            if (!unit.IsMe || tb == null)
            {
                return;
            }

            #region Combo
            if (SkyLv_Jax.Menu.Item("Jax.AfterAttackModeCombo").GetValue<bool>() && SkyLv_Jax.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)
            {
                var PacketCast = SkyLv_Jax.Menu.Item("Jax.UsePacketCastCombo").GetValue<bool>();
                var UseWCombo = SkyLv_Jax.Menu.Item("Jax.UseWCombo").GetValue<bool>();

                if (tb.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Player)))
                {
                    if (UseWCombo && W.IsReady())
                        W.Cast(PacketCast);
                }
                else return;

            }
            #endregion

            #region Harass
            if (SkyLv_Jax.Menu.Item("Jax.AfterAttackModeHarass").GetValue<bool>() && (SkyLv_Jax.Menu.Item("Jax.HarassActive").GetValue<KeyBind>().Active || SkyLv_Jax.Menu.Item("Jax.HarassActiveT").GetValue<KeyBind>().Active))
            {

                var PacketCast = SkyLv_Jax.Menu.Item("Jax.UsePacketCastHarass").GetValue<bool>();
                var UseWHarass = SkyLv_Jax.Menu.Item("Jax.UseWHarass").GetValue<bool>();

                var WMiniManaHarass = SkyLv_Jax.Menu.Item("Jax.WMiniManaHarass").GetValue<Slider>().Value;

                if (tb.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Player)))
                {
                    if (UseWHarass && W.IsReady() && Player.Mana >= W.ManaCost)
                        W.Cast(PacketCast);
                }
                else return;

            }
            #endregion

        }
    }
}
