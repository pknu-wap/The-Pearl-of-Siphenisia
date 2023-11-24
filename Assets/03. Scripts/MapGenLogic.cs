using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenLogic : MonoBehaviour
{
    static int stageLank = 0;

    string[,] stages = new string[5, 9] {
        { "stage1-1", "stage1-2", "stage1-3", "stage1-4", "stage1-5" , "stage1-6", "stage1-7", "stage1-8", "stage1-9"},
        { "stage2-1", "stage2-2", "stage2-3", "stage2-4", "stage2-5" , "stage2-6", "stage2-7", "stage2-8", "stage2-9"},
        { "stage3-1", "stage3-2", "stage3-3", "stage3-4", "stage3-5" , "stage3-6", "stage3-7", "stage3-8", "stage3-9"},
        { "stage4-1", "stage4-2", "stage4-3", "stage4-4", "stage4-5" , "stage4-6", "stage4-7", "stage4-8", "stage4-9"},
        { "stage5-1", "stage5-2", "stage5-3", "stage5-4", "stage5-5" , "stage5-6", "stage5-7", "stage5-8", "stage5-9"}};

    // 스테이지별 좌표 배열 선언

    public GameObject stage;
    public void Start()
    {

    }

    public void mapRandomGenerate()
    {
        string tmpStage = "null";
        int tmp;

        for (int i = stageLank; i < 5; i++) // 스테이지
        {
            for (int j = 0; j < 5; j++)     // 맵
            {
                tmp = Random.Range(1, 10);
                // 맵 클리어 작업 (클리어한 맵은 그대로 두도록 하는 clear)
                GameObject _stage = (GameObject)Resources.Load(tmpStage);
                Instantiate(_stage, GameObject.FindWithTag("Player").transform);    // GameObject.FindWithTag("Player").transform에 좌표[i,j] 넣을 예정
            }
        }
    }
}
