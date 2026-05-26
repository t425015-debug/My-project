using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Switch;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class RoadMapPLayer : MonoBehaviour
{

    [SerializeField] float moveSpeed;

    bool isMoving;
    Vector2 input;
    int lane = 0;
    Vector3 pos;


    float[] laneX =
    {
        -7.3f,
        -3.65f,
        0f,
        3.65f,
        7.3f
    };

    private void _LaneMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lane++;
        }

        // 範囲制限
        lane = Mathf.Clamp(lane, 0, 4);

        // レーン位置へ移動
        pos.x = laneX[lane];
    }


    public void HandleUpdate()
    {
        if (!isMoving)
        {
            _LaneMove();
            //キーボードの入力方向に動く
            input.x = Input.GetAxisRaw("Horizontal");

            // 斜め移動

            if (input != Vector2.zero)
            {
                Vector2 targetPos = transform.position;
                targetPos.x =  pos.x;
                StartCoroutine(Move(targetPos));
            }
        }
    }

   

    //コルーチンを使って徐々に目的地に近づける
    IEnumerator Move(Vector3 targetPos)
    {
        //移動中は入力を受け付けたくない

        isMoving = true;

        //targetPosとの差があるなら繰り返す
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            //targetposに近づける
            transform.position = Vector3.MoveTowards(
                transform.position,          //現在の場所
                targetPos,                   //目的地
                moveSpeed * Time.deltaTime)　 //近づけるスピード
                ;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }


    void Start()
    {
        isMoving = false;
        pos = transform.position;
    }

    void Update()
    {
        HandleUpdate();  
        _StageSelect();
    }

    private void _StageSelect()
    {
        switch (lane)
        {
            case 0: if (Input.GetKey(KeyCode.Space)) {
                    SceneManager.LoadScene("Stage1"); 
                } break;
            case 1: if (Input.GetKey(KeyCode.Space)) {
                    SceneManager.LoadScene("Stage2"); 
                } break;
            case 2: if (Input.GetKey(KeyCode.Space)) {
                    SceneManager.LoadScene("Stage3"); 
                } break;
            case 3: if (Input.GetKey(KeyCode.Space)) {
                    SceneManager.LoadScene("Stage4"); 
                } break;
            case 4: if (Input.GetKey(KeyCode.Space)) {
                    SceneManager.LoadScene("Stage5"); 
                } break;
        }
    }

}
