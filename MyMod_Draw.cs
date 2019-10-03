using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.HUD;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace MountedMagicMirrors {
	partial class MountedMagicMirrorsMod : Mod {
//private (int TileX, int TileY) LastMirror = (0,0);
		public override void PostDrawFullscreenMap( ref string mouseText ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<MMMPlayer>( Main.LocalPlayer );
			if( !myplayer.IsMirrorPicking ) {
				return;
			}

//bool isNew =	myplayer.TargetMirror.HasValue &&
//			(	myplayer.TargetMirror.Value.TileX != this.LastMirror.TileX ||
//				myplayer.TargetMirror.Value.TileY != this.LastMirror.TileY );
			foreach( (int tileX, int tileY) in myplayer.GetDiscoveredMirrors() ) {
				bool isTarget = myplayer.TargetMirror.HasValue &&
								myplayer.TargetMirror.Value.TileX == tileX &&
								myplayer.TargetMirror.Value.TileY == tileY;
//if( myplayer.TargetMirror.HasValue && isNew ) {
//this.LastMirror = myplayer.TargetMirror.Value;
//Main.NewText( "is target? "+myplayer.TargetMirror+" vs "+tileX+","+tileY );
//}
				
				this.DrawMirrorOnFullscreenMap( tileX, tileY, isTarget );
			}
		}


		public void DrawMirrorOnFullscreenMap( int tileX, int tileY, bool isTarget ) {
			Texture2D tex = this.MirrorTex;
			float myScale = isTarget ? 0.5f : 0.25f;
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