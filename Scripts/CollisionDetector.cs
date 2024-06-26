using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTrrigerEnter = new TriggerEvent();
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();

    /// <summary>
    /// Is TriggerがONで他のColliderと重なっているときは、
    /// このメソッドが常にコールされる
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        // onTriggerStayで指定された処理を実行する
        onTriggerStay.Invoke(other);
    }


    private void OnTriggerEnter(Collider other)
    {
        onTrrigerEnter.Invoke(other);
    }


    /// UnityEventを継承したクラスに[Serializable]属性を付与することで、
    /// Inspectorウインドウ上に表示できるようになる。
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
}