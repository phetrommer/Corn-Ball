using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_keyboard : MonoBehaviour
{
    private float heading = 0;
    public float speed;
    public Transform cam;
    public bool debugMode = false;

    Vector2 input;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "projectile")
        {
            if (debugMode == false)
            {
                gameObject.SetActive(false);
                FindObjectOfType<SceneManagerScript>().Invoke("GoToMainMenu", 2f);
                FindObjectOfType<GameManager>().currentRound = 0;
                FindObjectOfType<GameManager>().currentPoints = 0f;
            }
        }
    }

    void Update()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        if (Input.GetKey(KeyCode.LeftShift)) // sprint
        {
            transform.position += (camF * input.y + camR * input.x) * Time.deltaTime * (speed+20f);
        }
        else
        {
            transform.position += (camF * input.y + camR * input.x) * Time.deltaTime * speed;
        }
    }
}
