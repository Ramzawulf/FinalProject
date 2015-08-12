using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Generator : MonoBehaviour
{
	public Vector2 FloorSize = new Vector2 (300, 300);
	public int NumberOfIterations = 3;
	private Sprite LevelSprite;
	private SpriteRenderer SRenderer;
	private BSP bsp;

	void Start ()
	{
		/*if (Config.RandomSeed != 0)
			Random.seed = Config.RandomSeed;*/
		GenerateSprite ();
		Paint ();
		bsp = new BSP (FloorSize.x, FloorSize.y, 0, 0);
		for (int i = 0; i < NumberOfIterations; i++) {
			bsp.Grow ();
		}
		bsp.Paint (LevelSprite.texture);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	private void GenerateSprite ()
	{
		SRenderer = gameObject.AddComponent <SpriteRenderer> ();
		var tx = Texture2D.whiteTexture;
		tx.Resize ((int)FloorSize.x, (int)FloorSize.y);
		tx.filterMode = FilterMode.Point;
		var s = Sprite.Create (tx, new Rect (Vector2.zero, FloorSize), new Vector2 (0.5f, 0.5f), 1);
		s.name = "Dungeon";
		var tmpS = s;
		LevelSprite = tmpS;
		SRenderer.sprite = tmpS;
	}

	private void Paint ()
	{
		ClearPixels ();
	}

	private void ClearPixels ()
	{
		for (int x = 0; x < LevelSprite.texture.width; x++) {
			for (int y = 0; y < LevelSprite.texture.height; y++) {
				LevelSprite.texture.SetPixel (x, y, Color.white);
			}
		}
		LevelSprite.texture.Apply ();
	}
}
