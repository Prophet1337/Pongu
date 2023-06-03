using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private KeyCode UpKey;
    [SerializeField] private KeyCode DownKey;

    void Update()
    {
        if (Input.GetKey(UpKey))
        {
            transform.position += Vector3.up * Time.deltaTime * MovementSpeed;
        }
        if (Input.GetKey(DownKey))
        {
            transform.position += Vector3.down * Time.deltaTime * MovementSpeed;
        }
    }
}
