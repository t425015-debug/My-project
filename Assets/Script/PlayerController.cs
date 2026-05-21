using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector2 input;

        void Update()
    {
        //キーボードの入力方向に動く
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        transform.Translate(input.normalized * moveSpeed * Time.deltaTime);
    }
}
