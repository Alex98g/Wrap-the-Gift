using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class LevelInfo
	{
		private static readonly List<LevelInfo> levels = new List<LevelInfo>() {
			new LevelInfo() {
				Clients = new List<Client>() {
					new Client() {
						PresentColor = PresentColor.Blue,
						RibbonColor = RibbonColor.Yellow,
					},
					new Client() {
						PresentColor = PresentColor.Red,
						RibbonColor = RibbonColor.Green,
					},
					new Client() {
						PresentColor = PresentColor.Green,
						RibbonColor = RibbonColor.Blue,
					}
				},
				Time = new TimeSpan(0, 0, 45)
			},
			new LevelInfo() {
				Clients = new List<Client>() {
					new Client() {
						PresentColor = PresentColor.Red,
						RibbonColor = RibbonColor.Green,
					},
					new Client() {
						PresentColor = PresentColor.Yellow,
						RibbonColor = RibbonColor.Blue,
					},
					new Client() {
						PresentColor = PresentColor.Green,
						RibbonColor = RibbonColor.Blue,
					},
				},
				Time = new TimeSpan(0, 0, 45)
			},
			new LevelInfo() {
				Clients = new List<Client>() {
					new Client() {
						PresentColor = PresentColor.Yellow,
						RibbonColor = RibbonColor.Blue,
					},
					new Client() {
						PresentColor = PresentColor.Green,
						RibbonColor = RibbonColor.Red,
					},
					new Client() {
						PresentColor = PresentColor.Blue,
						RibbonColor = RibbonColor.Yellow,
					},
					new Client() {
						PresentColor = PresentColor.VividSkyBlue,
						RibbonColor = RibbonColor.RedCrayola,
					},
				},
				Time = new TimeSpan(0, 0, 45)
			},
			new LevelInfo() {
				Clients = new List<Client>() {
					new Client() {
						PresentColor = PresentColor.Blue,
						RibbonColor = RibbonColor.Yellow,
					},
					new Client() {
						PresentColor = PresentColor.VividSkyBlue,
						RibbonColor = RibbonColor.RedCrayola,
					},
					new Client() {
						PresentColor = PresentColor.Yellow,
						RibbonColor = RibbonColor.RedCrayola,
					},
					new Client() {
						PresentColor = PresentColor.Red,
						RibbonColor = RibbonColor.Green,
					},
				},
				Time = new TimeSpan(0, 0, 45)
			},
			new LevelInfo() {
				Clients = new List<Client>() {
					new Client() {
						PresentColor = PresentColor.Blue,
						RibbonColor = RibbonColor.Yellow,
					},
					new Client() {
						PresentColor = PresentColor.Yellow,
						RibbonColor = RibbonColor.Red,
					},
					new Client() {
						PresentColor = PresentColor.Green,
						RibbonColor = RibbonColor.Blue,
					},
					new Client() {
						PresentColor = PresentColor.Red,
						RibbonColor = RibbonColor.Green,
					},
					new Client() {
						PresentColor = PresentColor.Blue,
						RibbonColor = RibbonColor.Red,
					},
				},
				Time = new TimeSpan(0, 1, 0)
			},
		};

		public static LevelInfo GetLevel(int i)
		{
			return levels[i];
		}

		public int ClientCount => Clients.Count;

		public List<Client> Clients { get; private set; } = new List<Client>();

		public TimeSpan Time { get; private set; } = new TimeSpan(0, 1, 0);
	}
}