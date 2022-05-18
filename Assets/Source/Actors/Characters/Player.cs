﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Assets.Source.Actors.Static.Items;
using Assets.Source.Core;
using DungeonCrawl.Core;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;


namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        protected override void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                (int x, int y) currentPosition = (Position.x, Position.y);
                PickUpItem(ActorManager.Singleton.GetActorAt<YellowKey>(currentPosition));
                Debug.Log(currentPosition);
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        public static void PickUpItem(Item item)
        {
            if (item.DefaultName == "YellowKey" && Inventory.Count == 0)
            {
                //var pickedItem = item.Clone();
                Debug.Log("ifág");
                Inventory["YellowKey"] = item;
                ActorManager.Singleton.DestroyActor(item);
                UserInterface.Singleton.SetText(String.Empty, UserInterface.TextPosition.BottomRight);
                //Inventory.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Debug.Log);
                Debug.Log(YellowKey.Owned);
            }
            else if (item.DefaultName == "YellowKey" && Inventory.Count >= 1)
            {
                //var pickedItem = item.Clone();
                Debug.Log("elszág");
                YellowKey.Owned++;
                ActorManager.Singleton.DestroyActor(item);
                UserInterface.Singleton.SetText(String.Empty, UserInterface.TextPosition.BottomRight);
                //Inventory.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Debug.Log);
                Debug.Log(YellowKey.Owned);
            }
        }



        /// <summary>
        /// Ennek listának kellene lennie
        /// </summary>
        public static Dictionary<string, object> Inventory { get; set; } = new Dictionary<string, object>();
        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
