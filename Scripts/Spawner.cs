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
            // ����10�̃x�N�g��
            var distanceVector = new Vector3(10, 0);

            // �v���C���[�̈ʒu���x�[�X�ɂ����G�̏o���ʒu�BY���ɑ΂��ď�L�x�N�g���������_���ɂO�`�R�U�O�x��]������
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;

            // �G���o�����������ʒu������
            var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

            // �w��ʒu�����ԋ߂�NavMesh�̍��W
            NavMeshHit navMeshHit;
            if(NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
            {
                // enemyPrefab�𕡐��ANavMeshAgent�͕K��NavMesh��ɔz�u����
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
