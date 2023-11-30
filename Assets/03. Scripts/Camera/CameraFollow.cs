using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTr;
    Transform BG;

    public float followSpeed = 0.5f;

    Vector3 targetPos;
    public Vector3 offset = new(0f, 0f, 0f);
    [SerializeField] float minX;
    [SerializeField] float minY;
    [SerializeField] float maxX;
    [SerializeField] float maxY;
    float height;
    float width;

    private void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        BG = GameObject.FindWithTag("BG").transform;
        targetPos = new(0f, 0f, transform.position.z);

        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        minX = BG.position.x - (BG.localScale.x / 2) + (width / 2);
        minY = BG.position.y - (BG.localScale.y / 2) + (height / 2);
        maxX = BG.position.x + (BG.localScale.x / 2) - (width / 2);
        maxY = BG.position.y + (BG.localScale.y / 2) - (height / 2);
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
