using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField]Vector3 movementVector;
    float movementFactor; 
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // 시간에 따라 계속해서 증가함

        const float tau = Mathf.PI * 2; // 6.283으로 일정한 값
        float rawSinWave = Mathf.Sin(cycles * tau); // -1에서 1로 값이 올라갔다 내려온다.

        movementFactor = (rawSinWave + 1f) / 2; // 1을 더하고 2로 나눠 값을 0~1의 값으로 오르락내리락한다.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
