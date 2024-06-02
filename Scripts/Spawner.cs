using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }


    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            // 距離10のベクトル
            var distanceVector = new Vector3(10, 0);

            // プレイヤーの位置をベースにした敵の出現位置。Y軸に対して上記ベクトルをランダムに０～３６０度回転させる
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;

            // 敵を出現させたい位置を決定
            var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

            // 指定位置から一番近いNavMeshの座標
            NavMeshHit navMeshHit;
            if(NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
            {
                // enemyPrefabを複製、NavMeshAgentは必ずNavMesh上に配置する
                Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);

            }

            yield return new WaitForSeconds(10);

            if(playerStatus.Life <= 0)
            {
                break;
            }


        }
    }


}
