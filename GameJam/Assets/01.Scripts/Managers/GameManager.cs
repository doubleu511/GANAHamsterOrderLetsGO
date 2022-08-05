using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static PlayerController Player;
    public static GameManager Game;

    public CharacterSlotContainer CharacterSlotContainer;
    public PlayerInventoryContainer PlayerInventoryContainer;
    public DragAndDropContainer DragAndDropContainer;

    public Transform loreBlockTrm;

    private void Awake()
    {
        if (!Game)
        {
            Game = this;
        }

        Player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        GameObject loreBlock = Global.Resource.Load<GameObject>("UI/Loreblock");
        Global.Pool.CreatePool<LoreblockUI>(loreBlock, loreBlockTrm, 5);
    }
}
