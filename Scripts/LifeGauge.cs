using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    [SerializeField] private Image fillimage;

    private RectTransform _parentRectTransform;
    private Camera _camera;
    private MobStatus _status;


    public void Initialize(RectTransform rect, Camera cam, MobStatus mob)
    {
        _parentRectTransform = rect;
        _camera = cam;
        _status = mob;
        
    }

    private void Reflesh()
    {
        fillimage.fillAmount = _status.Life / _status.LifeMax;
        var screenPoint = _camera.WorldToScreenPoint(_status.transform.position);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null, out localPoint);
        transform.localPosition = localPoint + new Vector2(0, 80);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Reflesh();
    }
}
