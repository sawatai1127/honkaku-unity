using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTrrigerEnter = new TriggerEvent();
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();

    /// <summary>
    /// Is Trigger��ON�ő���Collider�Əd�Ȃ��Ă���Ƃ��́A
    /// ���̃��\�b�h����ɃR�[�������
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        // onTriggerStay�Ŏw�肳�ꂽ���������s����
        onTriggerStay.Invoke(other);
    }


    private void OnTriggerEnter(Collider other)
    {
        onTrrigerEnter.Invoke(other);
    }


    /// UnityEvent���p�������N���X��[Serializable]������t�^���邱�ƂŁA
    /// Inspector�E�C���h�E��ɕ\���ł���悤�ɂȂ�B
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
}