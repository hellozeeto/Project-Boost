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
        float cycles = Time.time / period; // �ð��� ���� ����ؼ� ������

        const float tau = Mathf.PI * 2; // 6.283���� ������ ��
        float rawSinWave = Mathf.Sin(cycles * tau); // -1���� 1�� ���� �ö󰬴� �����´�.

        movementFactor = (rawSinWave + 1f) / 2; // 1�� ���ϰ� 2�� ���� ���� 0~1�� ������ �������������Ѵ�.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
