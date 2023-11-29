using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform playerTr;
    Transform BG;

    public float followSpeed = 0.5f;

    Vector3 targetPos;
    public Vector3 offset = new(0f, 0f, 0f);
    [SerializeField] float minX = -30f;
    [SerializeField] float minY = -89.37f;
    [SerializeField] float maxX = 30f;
    [SerializeField] float maxY = -0.625f;

    private void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        BG = GameObject.Find("BG").transform;
        targetPos = new(0f, 0f, transform.position.z);

        minX = BG.position.x - (BG.localScale.x / 2);
        minY = BG.position.y - (BG.localScale.y / 2);
        maxX = BG.position.x + (BG.localScale.x / 2);
        maxY = BG.position.y + (BG.localScale.y / 2);
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        targetPos.x = Mathf.Clamp(playerTr.position.x, minX, maxX);
        targetPos.y = Mathf.Clamp(playerTr.position.y, minY, maxY);
        transform.position = targetPos + offset;
    }

    void SmoothFollowPlayer(float speed)
    {
        targetPos.x = Mathf.Clamp(playerTr.position.x, minX, maxX);
        targetPos.y = Mathf.Clamp(playerTr.position.y, minY, maxY);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed) + offset;
    }
}
