using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSorting : MonoBehaviour
{
    [SerializeField]
    private List<CollectionItem> collectionItems = new List<CollectionItem>();

    public void Setting()
    {
        GameManager.Game.maxGetCollectionCount = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            CollectionItem item = transform.GetChild(i).GetComponent<CollectionItem>();
            if (item != null)
            {
                item.collectionNumber = 1 << i;
                collectionItems.Add(item);
                GameManager.Game.maxGetCollectionCount++;
            }
        }
        Load();
    }
    public void Load()
    {

        GameManager.Game.curGetCollectionCount = 0;

        foreach (var item in collectionItems)
        {
            if ((GameManager.Game.collectionCount & item.collectionNumber) != 0)
            {
                item.gameObject.SetActive(false);
                GameManager.Game.curGetCollectionCount++;
            }
        }
    }

    [ContextMenu("RESET")]
    public void ResetData()
    {
        SecurityPlayerPrefs.DeleteKey("game.collection");
    }
}
