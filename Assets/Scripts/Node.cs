using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
	public Rect Rectangle;
	public int Level;
	public Room Room;
	public Node LeftChild;
	public Node RightChild;

	public Node (Vector2 dimension, Vector2 position)
	{
		Rectangle = new Rect (position, dimension);
		Rectangle.Set ((int)Rectangle.xMin, (int)Rectangle.yMin, (int)Rectangle.width, (int)Rectangle.height);
		LeftChild = null;
		RightChild = null;
		Room = null;
		Level = 0;
	}

	public void Split ()
	{
		if (IsLeaf ()) {
			var splitSize = Random.Range (Config.SplitMinPercentage, Config.SplitMaxPercentage);
			var splitChance = Random.Range (0.0f, 1.0f);
			if (splitChance <= Config.SplitHorizontalChance) {
				SplitHorizontally (splitSize);
				Debug.Log ("Horizontal split with chance: " + splitChance);
			} else {
				Debug.Log ("Vertical split with chance: " + splitChance);
				SplitVertically (splitSize);
			}
		} else {
			LeftChild.Split ();
			RightChild.Split ();
		}
	}

	public List<Node> GetLeafs ()
	{
		var result = new List<Node> ();
		if (LeftChild == null && RightChild == null)
			result.Add (this);
		else {
			result.AddRange (LeftChild.GetLeafs ());
			result.AddRange (RightChild.GetLeafs ());
		}
		return result;
	}

	public void GenerateRooms ()
	{
		if (IsLeaf ()) {
			Room = new Room (Rectangle);
		} else {
			RightChild.GenerateRooms ();
			LeftChild.GenerateRooms ();
		}
	}

	public bool IsLeaf ()
	{
		return LeftChild == null && RightChild == null;
	}

	#region Private

	private void SplitHorizontally (float splitSize)
	{
		var rcDimensions = new Vector2 (Rectangle.width, Rectangle.height * splitSize);
		var lcDimensions = new Vector2 (Rectangle.width, Rectangle.height * (1 - splitSize));

		var rcPosition = new Vector2 (Rectangle.xMin, Rectangle.yMin);
		var lcPosition = new Vector2 (Rectangle.xMin, Rectangle.yMin - lcDimensions.y);

		RightChild = new Node (rcDimensions, rcPosition);
		LeftChild = new Node (lcDimensions, lcPosition);
		RightChild.Level = Level + 1;
		LeftChild.Level = Level + 1;
	}
	private void SplitVertically (float splitSize)
	{
		var rcDimensions = new Vector2 (Rectangle.width * splitSize, Rectangle.height);
		var lcDimensions = new Vector2 (Rectangle.width * (1 - splitSize), Rectangle.height);
		
		var rcPosition = new Vector2 (Rectangle.xMin, Rectangle.yMin);
		var lcPosition = new Vector2 (Rectangle.xMin + lcDimensions.x, Rectangle.yMin);

		RightChild = new Node (rcDimensions, rcPosition);
		LeftChild = new Node (lcDimensions, lcPosition);
		RightChild.Level = Level + 1;
		LeftChild.Level = Level + 1;
	}
	#endregion Private
}
