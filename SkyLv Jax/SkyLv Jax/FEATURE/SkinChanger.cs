namespace SkyLv_Jax
{
    using System;

    using LeagueSharp;
    using LeagueSharp.Common;

    internal class SkinChanger
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


        static SkinChanger()
        {
            //Menu
            SkyLv_Jax.Menu.SubMenu("Misc").AddSubMenu(new Menu("Skin Changer", "Skin Changer"));
            SkyLv_Jax.Menu.SubMenu("Misc").SubMenu("Skin Changer").AddItem(new MenuItem("Jax.SkinChanger", "Use Skin Changer").SetValue(false));
            SkyLv_Jax.Menu.SubMenu("Misc").SubMenu("Skin Changer").AddItem(new MenuItem("Jax.SkinChangerName", "Skin choice").SetValue(new StringList(new[] 
            { "Original", "The Mighty", "Vandal", "Angler", "Pax", "Jaximus", "Temple", "Nemesis", "SKT T1", "Chroma 1", "Chroma 2", "Chroma 3", "Warden" })));

            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (SkyLv_Jax.Menu.Item("Jax.SkinChanger").GetValue<bool>())
            {
                Player.SetSkin(Player.CharData.BaseSkinName, SkyLv_Jax.Menu.Item("Jax.SkinChangerName").GetValue<StringList>().SelectedIndex);
            }
            else
                Player.SetSkin(Player.CharData.BaseSkinName, Player.BaseSkinId);
        }

        
    }
}
