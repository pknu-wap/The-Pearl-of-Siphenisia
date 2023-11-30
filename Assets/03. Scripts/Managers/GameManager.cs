using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int swordCount = 0;
    private GameObject tree;

    public UnityEvent onGameClear;

    private void Awake()
    {
        tree = GameObject.Find("TreeNRoot");
    }

    public void Count()
    {
        swordCount++;

        if(swordCount == 3)
        {
            // ø¨√‚
            DestroyTree();
        }
    }

    private void DestroyTree()
    {
        Destroy(tree);
    } 

    public void ClearGame()
    {
        GameUIManager.Instance.ShowGameClearUI();
        Time.timeScale = 0f;
    }
}
