using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget; // Player'? takip etmek i?in
    public Transform carTarget; // Arabay? takip etmek i?in
    public float smoothSpeed = 0.125f; // Kameran?n takip h?z?n? belirler
    public Vector2 offset; // Kameran?n takip etti?i objeye g?re konumunu belirleyen ofset

    private Transform currentTarget; // O anda takip edilen hedef

    void Start()
    {
        // Ba?lang??ta kameran?n player'? takip etmesini sa?l?yoruz
        currentTarget = playerTarget;
    }

    void Update()
    {
        // E?er player arabaya bindi?inde bir kontrol varsa, bu kontrolle target'? de?i?tirebilirsin.
        if (Input.GetKeyDown(KeyCode.E))
        {
            // E?er currentTarget player ise arabaya ge?, de?ilse player'a ge?
            if (currentTarget == playerTarget)
            {
                currentTarget = carTarget; // Player arabaya bindi, araba hedef
            }
            else
            {
                currentTarget = playerTarget; // Player arabadan indi, player hedef
            }
        }
    }

    void LateUpdate()
    {
        if (currentTarget != null)
        {
            // Yaln?zca X ve Y eksenlerini takip edecek ?ekilde pozisyonu ayarl?yoruz (2D oyun i?in)
            Vector3 desiredPosition = new Vector3(currentTarget.position.x + offset.x, currentTarget.position.y + offset.y, -10f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}