using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int swordCount = 0;
    private GameObject tree;

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
}
