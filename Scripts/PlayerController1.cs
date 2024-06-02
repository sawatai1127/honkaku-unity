using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// �w��̃R���|�[�l���g���K���A�^�b�`����Ă��邱�Ƃ�錾����B�K�{����Ȃ�
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]
public class PlayerController1 : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed = 3;    // �ړ����x
    [SerializeField] private float jumpPower = 3;    // �W�����v��
    private CharacterController _characterController;

    //�@CharacterController�̃L���b�V��
    private Transform _transform;   // transformer�L���b�V��
    private Vector3 _moveVelocity;  // �L�����ړ����

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
        // Debug.Log(_characterController.isGrounded ? "�n��ɂ��܂��B" : "�󒆂ɂ��܂��B");
        Debug.Log(IsGrounded ? "�n��ɂ��܂��B" : "�󒆂ɂ��܂��B");

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            _attack.AttackIfPossible();
        }

        if (_status.IsMovable)
        {

            // ���݂̈ړ����x���擾
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
            // �n�ォ�H
            if (Input.GetButtonDown("Jump"))
            {

                Debug.Log("�W�����v�I");
                _moveVelocity.y = jumpPower;
            }
        }
        else
        {
            // �󒆂��H
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        //�@_moveVelocity�ɉ��Z�����ړ����x�ŃL�������ړ�
        _characterController.Move(_moveVelocity * Time.deltaTime);

        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}
