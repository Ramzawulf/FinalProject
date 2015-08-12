using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Room
{
	public Rect Dimensions{ get; private set; }
	public Vector2 LowerLeftCorner{ get { return Dimensions.min; } }
	
	
	public Room (Rect rect)
	{
		Dimensions = rect;
	}
	
	public void Paint (Sprite sprite)
	{
		var tx = sprite.texture;
		var colors = new Color[(int)Dimensions.x * (int)Dimensions.y];
		for (int i = 0; i < colors.Length; i++) {
			colors [i] = Color.black;
		}
		
		tx.SetPixels ((int)LowerLeftCorner.x, (int)LowerLeftCorner.y, (int)Dimensions.x, (int)Dimensions.y, colors);
		tx.Apply ();
	}
}
