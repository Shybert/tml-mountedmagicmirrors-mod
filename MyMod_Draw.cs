using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.HUD;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace MountedMagicMirrors {
	partial class MountedMagicMirrorsMod : Mod {
		public override void PostDrawFullscreenMap( ref string mouseText ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<MMMPlayer>( Main.LocalPlayer );

			foreach( (int tileX, int tileY) in myplayer.GetDiscoveredMirrors() ) {
				this.DrawMirrorOnFullscreenMap( tileX, tileY );
			}
		}


		public void DrawMirrorOnFullscreenMap( int tileX, int tileY ) {
			Texture2D tex = this.MirrorTex;
			float myScale = 0.25f;
			float uiScale = Main.mapFullscreenScale;//( isZoomed ? Main.mapFullscreenScale : 1f ) * scale;

			int wldX = ( tileX * 16 ) - (int)( (float)tex.Width * 8f * myScale );
			int wldY = ( tileY * 16 ) - (int)( (float)tex.Height * 8f * myScale );
			int wid = (int)( (float)tex.Width * myScale );
			int hei = (int)( (float)tex.Height * myScale );

			var wldRect = new Rectangle( wldX, wldY, wid, hei );
			var overMapData = HUDMapHelpers.GetFullMapScreenPosition( wldRect );

			//DebugHelpers.Print( "mapdraw", "tileX:"+tileX+", tileY:"+tileY+
			//	", plrpos: " + (int)Main.LocalPlayer.Center.X+":"+(int)Main.LocalPlayer.Center.Y+
			//	", wldRect:" + wldRect+", overMapData:" + overMapData.Item1, 20 );
			if( overMapData.Item2 ) {
				Main.spriteBatch.Draw(
					tex,
					overMapData.Item1,
					null,
					Color.White,
					0f,
					default( Vector2 ),
					uiScale * myScale,
					SpriteEffects.None,
					1f
				);
			}
		}
	}
}