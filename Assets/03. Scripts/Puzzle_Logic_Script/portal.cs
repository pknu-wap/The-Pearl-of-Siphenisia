using UnityEngine;

public class portal : MonoBehaviour
{
    public Transform destinationPortal; // 다른 포탈의 Transform을 할당하여 이동할 포탈 설정
    public Vector3 teleportOffset; // 포탈 이동 후 플레이어 위치 보정을 위한 오프셋

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TeleportObject(other.gameObject);
        }
    }

    void TeleportObject(GameObject _objact)  // 텔레포트 함수
    {
        if (destinationPortal != null)      // 다른 포탈을 지정했다면 그 포탈의 (x, y + 오프셋) 으로 순간이동
        {
            Vector3 destinationPosition = destinationPortal.position + teleportOffset;
            _objact.transform.position = destinationPosition;
        }
        else                                // 포탈 지정이 되지 않았다면 에러메시지
        {
            Debug.LogWarning("다른 포탈이 지정되지 않았습니다.");
        }
    }
}
