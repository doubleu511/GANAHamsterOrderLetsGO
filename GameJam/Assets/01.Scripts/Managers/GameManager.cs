using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static PlayerController Player;
    public static GameManager Global;

    public CharacterSlotContainer CharacterSlotContainer;
    public PlayerInventoryContainer PlayerInventoryContainer;
    public DragAndDropContainer DragAndDropContainer;

    private void Awake()
    {
        if (!Global)
        {
            Global = this;
        }

        Player = FindObjectOfType<PlayerController>();
    }
}
