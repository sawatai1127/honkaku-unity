using UnityEngine;

public abstract class MobStatus : MonoBehaviour
{
    // キャラクター状態定義
    protected enum StateEnum
    {
        Normal,
        Attack,
        Die
    }

    // 移動可能
    public bool IsMovable => StateEnum.Normal == _state;

    public bool IsAttackable => StateEnum.Normal == _state;

    // ライフ最大値取得
    public float LifeMax => lifeMax;

    // ライフ最大値
    public float Life => _life;

    [SerializeField] private float lifeMax = 10;   // 設定可能なライフ最大値

    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal;

    private float _life;    //  現在のライフ

    protected virtual void Start()
    {
        _life = lifeMax;
        _animator = GetComponent<Animator>();

        LifeGaugeContainer.Instance.Add(this);
    }


    protected virtual void OnDie()
    {
        LifeGaugeContainer.Instance.Remove(this);
    }


    public void Damage(int damage)
    {
        if(_state == StateEnum.Die)
        {
            return;
        }

        _life -= damage;
        if( _life > 0)
        {
            return;
        }

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");

        OnDie();

    }

    public void GoToAttackSatteIfPossible()
    {
        if (!IsAttackable)
        {
            return;
        }

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }


    public void GoToNormalSatteIfPossible()
    {
        if(_state == StateEnum.Die)
        {
            return;
        }

        _state = StateEnum.Normal;
    }

}
