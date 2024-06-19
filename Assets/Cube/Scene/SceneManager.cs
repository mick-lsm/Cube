using Cube.Components;
using Cube.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;

namespace Cube.Scene
{
    public class SceneManager : Utils.Singleton<SceneManager>
    {
        public List<Vector2> spawnPoints;
        public List<GameObject> players;
        public List<GameObject> enemies;
        public List<GameObject> entities;
        [HideInInspector]
        public List<GameObject> doors;

        public List<Collider2D> platforms;
        public GameObject[] mapPrefabs;
        public GameObject currentMap;

        public event Action OnChangedMap;

        private void Start()
        {
            OnChangedMap += ResetPlatforms;
            OnChangedMap += () => StartCoroutine(nameof(ResetSpawnPoints));
            //OnChangedMap += ResetDoors;
            //OnChangedMap += () => StartCoroutine(SetPlayerToDoor());
            SetRandomMap();
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (a,b) => SetRandomMap();
            //ResetPlatforms();
        }
        private void ResetPlatforms()
        {
            platforms.Clear();
            for (var i = 0; i < currentMap.transform.childCount; i++)
            {
                var child = currentMap.transform.GetChild(i);
                if (!child.TryGetComponent<Collider2D>(out var coll) || child.gameObject.name != "Platform") continue;
                platforms.Add(coll);
            }

            //var ps = FindObjectsOfType<Collider2D>();
            //foreach(var p in ps)
            //{
            //    Debug.Log(p.gameObject.name);
            //}
            //platforms = Array.Empty<Collider2D>();
            //platforms = ps.Where(p => p.gameObject.name == "Platform").ToArray();
        }
        private IEnumerator ResetSpawnPoints()
        {
            yield return null;
            spawnPoints.Clear();
            var dir = Vector2.down;
            
            for (var i = -50f; i < 50f; i++)
            {
                for (var j = -20; j < 15f; j++)
                {
                    var start = new Vector2(i, j);
                    var ray = Physics2D.Raycast(start, dir, 2f);
                    if (ray.collider == null) continue;
                    if (!ray.collider.TryGetComponent<Spawnable>(out var _)) continue;
                    var overlap = Physics2D.OverlapCircle(start, 0.5f);
                    if (overlap != null) continue;
                    //if (!overlap.TryGetComponent<Spawnable>(out var _)) continue;
                    spawnPoints.Add(start);
                    //Debug.Log(start);
                    //Debug.Log(ray.collider);
                    //if (ray.collider == null) continue;
                    //if (ray.distance > 1) continue;
                    //if (ray.distance <= 0.3f) continue;
                    //spawnPoints.Add(start);
                }
            }
        }
        private void ResetDoors()
        {
            doors.Clear();
            //Debug.Log(currentMap.transform.childCount);
            var children = currentMap.transform.GetComponentsInChildren<Transform>();
            foreach(var child in children)
            {
                if (child.name != "Door") continue;
                doors.Add(child.gameObject);
            }
            //for (var i = 0; i < currentMap.transform.childCount; i++)
            //{
            //    var child = currentMap.transform.GetChild(i);
            //    Debug.Log(child.name);
            //    if (child.gameObject.name != "Door") continue;
            //    doors.Add(child.gameObject);
            //}
        }

        [ContextMenu("Set Random Map")]
        public void SetRandomMap()
        {
            var index = UnityEngine.Random.Range(0, mapPrefabs.Length);

            if(currentMap != null) Destroy(currentMap);
            currentMap = Instantiate(mapPrefabs[index]);

            Debug.Log(index);

            OnChangedMap?.Invoke();
        }
        private IEnumerator SetPlayerToDoor()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            players.ForEach(p => p.transform.position = doors.First().transform.position);
        }
        public void RegisterPlayer(GameObject player)
        {
            players.Add(player);
            entities.Add(player);
        }
        public void UnregisterPlayer(GameObject player)
        {
            players.Remove(player);
            entities.Remove(player);
        }

        public void RegisterEnemy(GameObject enemy)
        {
            enemies.Add(enemy);
            entities.Add(enemy);
        }
        public void UnregisterEnemy(GameObject enemy)
        {
            enemies.Remove(enemy);
            entities.Remove(enemy);
        }
    }
}
