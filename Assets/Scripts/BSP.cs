using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class BSP
{
	public Node Root;
	public int Length;

	public BSP (float w, float h, float x, float y)
	{
		Root = new Node (new Vector2 (w, h), new Vector2 (x, y));
		Root.Level = 0;
		Length = 0;
	}

	public void Grow ()
	{
		Root.Split ();
		var l = Root.GetLeafs ();
		Length++;
	}

	public void Paint (Texture2D tx)
	{
		var leafs = Root.GetLeafs ();

		foreach (var l in leafs) {

			tx.SetPixel ((int)l.Rectangle.center.x, (int)l.Rectangle.center.y, Color.black);
			tx.Apply ();
			/*GameObject go = new GameObject ();
			SpriteRenderer renderer = go.AddComponent<SpriteRenderer> ();
			var s = Resources.Load ("Sprites/Generic") as Sprite;
			renderer.sprite = s;*/

			/*var tx = Texture2D.whiteTexture;
			tx.Resize ((int)l.Rectangle.width, (int)l.Rectangle.height);
			tx.filterMode = FilterMode.Point;
			Sprite.Create (tx, l.Rectangle, new Vector2 (0.5f, 0.5f));*/
		}
	}

	public bool CanGrow ()
	{
		return true;
	}
}
