using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private float targetAngle = 0.0f;

    public float playerRotationSpeed = 4f;



    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0) /*&& !EventSystem.current.IsPointerOverGameObject()*/)
        {
            Debug.Log("MousePosition" + Input.mousePosition);

            targetAngle = GetRotationAngleByTargetPosition(Input.mousePosition);
        }

#elif UNITY_IOS && UNITY_ANDROID
        
        //TODO: 後でスマホ操作も追加する

#endif

        this.transform.eulerAngles = new Vector3(0,0,Mathf.LerpAngle(this.transform.eulerAngles.z, targetAngle, Time.deltaTime * playerRotationSpeed));


    }

    /// <summary>
    /// プレイヤーとターゲットの角度を計算して度数法に変換した値を返す
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <returns></returns>
    float GetRotationAngleByTargetPosition(Vector3 mousePosition)
    {
        //マウスのポジションをワールド座標からスクリーン座標に変換する
        Vector3 selfScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        //スクリーン座標に変換したマウスポジションとプレイヤーポジションの座標の差分を計算する
        Vector3 diff = mousePosition - selfScreenPosition;

        //プレイヤーの位置を原点としたときのマウスポジションの角度を計算し度数法に変換する
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        float finalAngle = angle - 90f;

        return finalAngle;
    }


}