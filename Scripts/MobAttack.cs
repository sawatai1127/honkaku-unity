using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Collider attackCollider;
    [SerializeField] private AudioSource swingSound;

    private MobStatus _status;

    private void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;

        _status.GoToAttackSatteIfPossible();

    }

    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }


    public void OnAttackStart()
    {
        attackCollider.enabled = true;

        if(swingSound != null)
        {
            swingSound.pitch = Random.Range(0.7f, 1.3f);
            swingSound.Play();

        }
    }

    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<MobStatus>();
        if( null == targetMob)
        {
            return;
        }

        targetMob.Damage(1);
    }

    public void OnAttackFinish()
    {
        attackCollider.enabled = false;

        StartCoroutine(CooldownCorutine());
    }

    private IEnumerator CooldownCorutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalSatteIfPossible();
    }
}
