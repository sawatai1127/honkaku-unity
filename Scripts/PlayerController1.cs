using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// 指定のコンポーネントが必ずアタッチされていることを宣言する。必須じゃない
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]
public class PlayerController1 : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed = 3;    // 移動速度
    [SerializeField] private float jumpPower = 3;    // ジャンプ力
    private CharacterController _characterController;

    //　CharacterControllerのキャッシュ
    private Transform _transform;   // transformerキャッシュ
    private Vector3 _moveVelocity;  // キャラ移動情報

    private PlayerStatus _status;
    private MobAttack _attack;

    private bool IsGrounded
    {
        get
        {
            var ray = new Ray(_transform.position + new Vector3(0, 0.1f), Vector3.down);

            var raychastHits = new RaycastHit[1];
            var hitCount = Physics.RaycastNonAlloc(ray, raychastHits, 0.2f);
            return hitCount >= 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(_characterController.isGrounded ? "地上にいます。" : "空中にいます。");
        Debug.Log(IsGrounded ? "地上にいます。" : "空中にいます。");

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            _attack.AttackIfPossible();
        }

        if (_status.IsMovable)
        {

            // 現在の移動速度を取得
            _moveVelocity.x = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
            _moveVelocity.z = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed;

            // ??
            transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.y));

        }
        else
        {
            _moveVelocity.x = 0;
            _moveVelocity.z = 0;

        }



        // if (_characterController.isGrounded)
        if (IsGrounded)
        {
            // 地上か？
            if (Input.GetButtonDown("Jump"))
            {

                Debug.Log("ジャンプ！");
                _moveVelocity.y = jumpPower;
            }
        }
        else
        {
            // 空中か？
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        //　_moveVelocityに加算した移動速度でキャラを移動
        _characterController.Move(_moveVelocity * Time.deltaTime);

        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}
