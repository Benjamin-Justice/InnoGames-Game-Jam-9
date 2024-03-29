﻿using UnityEngine;
using System.Collections.Generic;
using GT = MiniGames.GroundTypeEnum;
using System;

namespace MiniGames.IceSliding
{
	public class Layout : MonoBehaviour
	{
		public GameObject ChildContainer;
		public GameObject NorthButton;
		public GameObject EastButton;
		public GameObject SouthButton;
		public GameObject WestButton;
		public Sprite RockSprite;
		public Sprite StartSprite;
		public Sprite FinishSprite;
		public Sprite IceSprite;
		public Sprite DirtSprite;
		Position2d playerPosition;
		GT[,] Landscape;
		List<GT[,]> boardList = new List <GT[,]> ();

		const float multiplier = 1.28f;

		void Awake ()
		{
			boardList.Add (new GT[,] {
				{ GT.SRT, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.WAL },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__D, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.WAL, GT.FIN, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL },
			});
			boardList.Add (new GT[,] {
				{ GT.SRT, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.FIN, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__D, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL },
			});
			boardList.Add (new GT[,] {
				{ GT.__I, GT.__I, GT.__I, GT.FIN, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL },
				{ GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.WAL, GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL },
				{ GT.WAL, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.WAL },
				{ GT.__I, GT.__I, GT.__I, GT.__I, GT.__I, GT.WAL, GT.__I, GT.__I, GT.__I, GT.SRT },
			});
			Landscape = boardList [UnityEngine.Random.Range (0, boardList.Count)];
		}

		void Start ()
		{
			for (int i = 0; i < Landscape.GetLength (0); i++) {
				for (int j = 0; j < Landscape.GetLength (1); j++) {
					GameObject child = new GameObject ("Child " + i + " " + j);
					SpriteRenderer render = child.AddComponent<SpriteRenderer> ();
					switch (Landscape [i, j]) {
					case GT.__I:
						render.sprite = IceSprite;
						break;
					case GT.__D:
						render.sprite = DirtSprite;
						break;
					case GT.WAL:
						render.sprite = RockSprite;
						break;
					case GT.SRT:
						render.sprite = StartSprite;
						break;
					case GT.FIN:
						render.sprite = DirtSprite;
						GameObject flag = new GameObject ("Flag " + i + " " + j);
						SpriteRenderer finishSpriteRenderer = flag.AddComponent<SpriteRenderer> ();
						finishSpriteRenderer.sortingOrder = 1;
						finishSpriteRenderer.sprite = FinishSprite;
						flag.transform.parent = child.transform;
						break;
					default:
						throw new ArgumentOutOfRangeException ();
					}
					child.transform.parent = this.ChildContainer.transform;
					child.transform.position = IntToFloat (i, j);
				}
			}
		}

		public Vector3 getStartPosition ()
		{
			for (int i = 0; i < Landscape.GetLength (0); i++) {
				for (int j = 0; j < Landscape.GetLength (1); j++) {
					if (Landscape [i, j].Equals (GT.SRT)) {
						Position2d pos = new Position2d (i, j);
						playerPosition = pos;
						return IntToFloat (pos);
					}
				}
			}
			throw new ArgumentException ("no Start defined");
		}

		public bool ReachedFinish ()
		{
			return playerPosition != null && Landscape [playerPosition.x, playerPosition.y].Equals (GT.FIN);
		}

		static Vector3 IntToFloat (Position2d pos)
		{
			return IntToFloat (pos.x, pos.y);
		}

		static Vector3 IntToFloat (int x, int y)
		{
			return new Vector3 (y * multiplier, -x * multiplier);
		}

		public  Vector3 Move (DirectionEnum direction)
		{
			Position2d newPos = new Position2d (playerPosition.x, playerPosition.y);
			Position2d positionChange = new Position2d (0, 0);
			switch (direction) {
			case DirectionEnum.NORTH:
				positionChange.x = -1;
				break;
			case DirectionEnum.EAST:
				positionChange.y = 1;
				break;
			case DirectionEnum.SOUTH:
				positionChange.x = 1;
				break;
			case DirectionEnum.WEST:
				positionChange.y = -1;
				break;
			default:
				throw new System.ArgumentOutOfRangeException ();
			}
			do {
				newPos.AddLocal (positionChange);
			} while (posInBounds (newPos)
			         && Landscape [newPos.x, newPos.y].Equals (GT.__I));
			if (!posInBounds (newPos) || Landscape [newPos.x, newPos.y].Equals (GT.WAL)) {
				newPos.AddLocal (positionChange.negateLocal ());
			}
			playerPosition = newPos;
			return IntToFloat (newPos);
		}

		bool posInBounds (Position2d newPos)
		{
			return newPos.x >= 0 && newPos.y >= 0 && newPos.x < Landscape.GetLength (0) && newPos.y < Landscape.GetLength (1);
		}

		public void SetButtonsActive (bool b)
		{
			if (b) {
				Position2d up = new Position2d (playerPosition.x - 1, playerPosition.y);
				if (posInBounds (up) && !Landscape [up.x, up.y].Equals (GT.WAL)) {
					NorthButton.SetActive (true);
				}
				Position2d down = new Position2d (playerPosition.x + 1, playerPosition.y);
				if (posInBounds (down) && !Landscape [down.x, down.y].Equals (GT.WAL)) {
					SouthButton.SetActive (true);
				}
				Position2d left = new Position2d (playerPosition.x, playerPosition.y - 1);
				if (posInBounds (left) && !Landscape [left.x, left.y].Equals (GT.WAL)) {
					WestButton.SetActive (true);
				}
				Position2d right = new Position2d (playerPosition.x, playerPosition.y + 1);
				if (posInBounds (right) && !Landscape [right.x, right.y].Equals (GT.WAL)) {
					EastButton.SetActive (true);
				}
			} else {
				NorthButton.SetActive (false);
				SouthButton.SetActive (false);
				WestButton.SetActive (false);
				EastButton.SetActive (false);
			}
		}
	}
}