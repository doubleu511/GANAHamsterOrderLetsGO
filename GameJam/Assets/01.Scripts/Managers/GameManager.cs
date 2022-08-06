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
    public PhysicsMaterial2D staticPMat;
    public PhysicsMaterial2D bouncePMat;

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

        // 자막실행
        SubtitlePanel subtitle = FindObjectOfType<SubtitlePanel>();

        int gameStartCount = SecurityPlayerPrefs.GetInt("game.startCount", 0);
        gameStartCount++;

        if(gameStartCount == 1)
        {
            subtitle.ShowSubtitle(subtitle.onFirstStart);
        }
        else
        {
            subtitle.ShowSubtitle(subtitle.onReenter);
        }

        SecurityPlayerPrefs.SetInt("game.startCount", gameStartCount);
    }
}
