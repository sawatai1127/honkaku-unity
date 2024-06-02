using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Wood,
        Stone,
        ThrowAxe
    }


    [SerializeField] private ItemType type;


    public void Initialize()
    {
        var collideerCache = GetComponent<Collider>();
        collideerCache.enabled = false;

        var transformCache = transform;
        var dropPosition = transform.localPosition + 
            new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        transformCache.DOLocalMove(dropPosition, 0.5f);

        var defaulScale = transformCache.localScale;
        transformCache.DOScale(defaulScale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            collideerCache.enabled = true;
        });



    }




    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        OwnedItemsData.Instance.Add(type);
        OwnedItemsData.Instance.Save();

        foreach( var item in OwnedItemsData.Instance.OwnedItems)
        {
            Debug.Log(item.Type + "Ç" + item.Number + "å¬èäéù");
        }


        Destroy(gameObject);
    }

}
