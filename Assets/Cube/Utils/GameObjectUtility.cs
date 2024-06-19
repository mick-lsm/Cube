using Cube.Components;
using Cube.Entity;
using Cube.Entity.Behaviours;
using Cube.Registry;
using Cube.Scene;
using Cube.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Cube.Utils
{
    public static class GameObjectUtility
    {
        private static Collider2D PlatformCollider;//= GameObject.Find("Platform").GetComponent<Collider2D>();


        public static Vector2 DirectionTo2D(this Vector2 at, Vector2 look)
        {
            return (look - at).normalized;
        }
        public static void TargetAt2D(this Transform tfm, Vector2 look)
        {
            var dir = DirectionTo2D(tfm.transform.position, look);
            tfm.right = dir;
        }
        public static WeaponOnGround PackageWeapon(this WeaponBehaviour weapon)
        {
            var wogObj = new GameObject(weapon.gameObject.name + "OnGround");
            var des = weapon.GetDescription();

            var wog = wogObj.AddComponent<WeaponOnGround>();
            wog.source = weapon;

            var ren = wogObj.AddComponent<SpriteRenderer>();
            ren.sprite = des.sprite;
            ren.sortingLayerName = "Weapon";

            var col = wogObj.AddComponent<BoxCollider2D>();
            col.isTrigger = true;

            var tfm = wogObj.transform;
            tfm.position = weapon.transform.position;

            var wTfm = weapon.transform;
            wTfm.SetParent(tfm);
            wTfm.localPosition = Vector3.zero;

            weapon.gameObject.SetActive(false);

            return wog;
        }
        public static Team GetTeam(this GameObject obj)
        {
            if (obj.CompareTag("Player")) return Team.Player;
            if (obj.CompareTag("Enemy")) return Team.Enemy;
            if (obj.CompareTag("Obstacle")) return Team.Global;
            return Team.Ignore;
        }
        public static bool IsOpontTo(this Team team1, Team team2)
        {
            if (team1 == Team.Ignore) return false;
            if (team2 == Team.Ignore) return false;
            if (team1 == Team.Global && team2 == Team.Global) return false;
            return team1 != team2;
        }
        public static bool IsObstacle(this GameObject obj)
        {
            //if (obj.name == "Platform") return false;
            //if (obj.name == "Ground") return true;
            if (obj.CompareTag("Obstacle")) return true;
            return false;
        }
        public static Color GetRepresentativeColor(this GameObject obj)
        {
            if (obj.CompareTag("Player")) return Color.cyan;
            if (obj.CompareTag("Enemy")) return Color.red;
            throw new ArgumentException("No color for this gameObject! GameObject : " + obj.name);
        }
        public static float[] EvenlyDistributeLinear1On0(int count)
        {
            var points = new float[count];

            var interval = 2f / (count - 1);

            for (int i = 0; i < count; i++)
            {
                points[i] = (interval * i) - 1;
            }
            return points;
        }
        public static GameObject GetNearestPlayer(this Transform tfm)
        {
            var players = SceneManager.Instance.players;
                //GameSceneRegister.Players;
            var ordered = players.OrderBy(p => Vector2.Distance(p.transform.position,tfm.transform.position));
            var first = ordered.FirstOrDefault();
            return first;
        }
        public static void IgnoreToPlatform(this Collider2D coll, bool ignore)
        {
            //if(PlatformCollider == null) PlatformCollider = GameObject.Find("Platform").GetComponent<Collider2D>();
            foreach (var platform in SceneManager.Instance.platforms)
            {
                Physics2D.IgnoreCollision(coll, platform, ignore);
            }
        }
        public static bool IsDamageable(this GameObject obj)
        {
            return obj.TryGetComponent<Damageable>(out var _);
            //SceneManager.Instance.entities.Contains(obj);
            //GameSceneRegister.Entities.Contains(obj);
        }
        public static int CreateRoundRaycastCollision(Vector2 worldPos, float radius, out Collider2D[] results)
        {
            var colliders = new Collider2D[20];
            var count = Physics2D.OverlapCircleNonAlloc(worldPos, radius, colliders);

            results = new Collider2D[20];
            Physics2D.queriesHitTriggers = false;
            for(var i = 0; i < count; i++)
            {
                var pos = colliders[i].transform.position;
                var rayCast = Physics2D.Raycast(worldPos, pos);
                if (rayCast.collider.gameObject.IsDamageable()) results[i] = colliders[i];
                else results[i] = rayCast.collider;
            }
            Physics2D.queriesHitTriggers = true;
            return count;
        }
    }
}
