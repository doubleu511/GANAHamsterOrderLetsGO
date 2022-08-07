using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSorting : MonoBehaviour
{
    [SerializeField]
    private List<CollectionItem> collectionItems = new List<CollectionItem>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            CollectionItem item = transform.GetChild(i).GetComponent<CollectionItem>();
            if(item != null)
            {
                item.collectionNumber = 1 << i;
                collectionItems.Add(item);
            }
        }


        foreach (var item in collectionItems)
        {
            if(UtilClass.IsIncludeFlag<int>(GameManager.Game.collectionCount,item.collectionNumber))
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
