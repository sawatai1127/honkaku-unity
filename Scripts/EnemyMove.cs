using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask;

    private NavMeshAgent _agent;
    private EnemyStatus _status;

    private RaycastHit[] _raycastHits = new RaycastHit[10];

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent��ێ����Ă���
        _status = GetComponent<EnemyStatus>();
    }

    // CollisionDetector��onTriggerStay�ɃZ�b�g���A�Փ˔�����󂯎�郁�\�b�h
    public void OnDetectObject(Collider collider)
    {
        if(_status == null)
        {
            return;
        }

        if (!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }

        // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position;

            var distans = positionDiff.magnitude;

            var directioin = positionDiff.normalized;

           //  var hitCount = Physics.RaycastNonAlloc(transform.position, directioin, _raycastHits, distans, raycastPlayerMask);
            var hitCount = Physics.RaycastNonAlloc(transform.position, directioin, _raycastHits, distans, raycastLayerMask);

            //Ray ray = new Ray(transform.position, directioin); // Ray�𐶐�
            //Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 5.0f); // �����R�O�A�ԐF�łT�b�ԉ���

            Debug.Log("hitCount : " + hitCount);

            if(hitCount == 0)
            {
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                _agent.isStopped = true;
            }
        }
    }
}