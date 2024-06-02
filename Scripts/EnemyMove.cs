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
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgentを保持しておく
        _status = GetComponent<EnemyStatus>();
    }

    // CollisionDetectorのonTriggerStayにセットし、衝突判定を受け取るメソッド
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

        // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position;

            var distans = positionDiff.magnitude;

            var directioin = positionDiff.normalized;

           //  var hitCount = Physics.RaycastNonAlloc(transform.position, directioin, _raycastHits, distans, raycastPlayerMask);
            var hitCount = Physics.RaycastNonAlloc(transform.position, directioin, _raycastHits, distans, raycastLayerMask);

            //Ray ray = new Ray(transform.position, directioin); // Rayを生成
            //Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 5.0f); // 長さ３０、赤色で５秒間可視化

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