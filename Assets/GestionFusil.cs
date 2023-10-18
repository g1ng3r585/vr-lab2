using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionFusil : MonoBehaviour
{
    // rayon
    [SerializeField] private Transform origin;
    private float distance = 10f;

    // sons
    [SerializeField] private AudioSource pew;
    [SerializeField] private AudioSource pouf;

    // gestion du temps
    private float startTime;
    private float endTime;
    private bool firstShotFired;
    private List<float> balloonExplosionTimes;

    private InputAction action;

    public void OnEnable()
    {
        action = new InputAction("Trigger", binding: "<XRcontroller>{RightHand}/trigger");
        action.Enable();
        action.performed += TriggerPerformed;
    }

    private void TriggerPerformed(InputAction.CallbackContext context)
    {
        // Trigger press logic
        if (context.ReadValue<float>() > 0.5f)
        {
            // Handle the trigger press here
            // For example, you can call a shooting function.
            Fire();
        }
    }

    private void OnDisable()
    {
        action.Disable();
        action.performed -= TriggerPerformed;
    }

    private void Start()
    {
        balloonExplosionTimes = new List<float>();
        firstShotFired = false;
    }

    private void Fire()
    {
        if (!firstShotFired)
        {
            startTime = Time.time;
            firstShotFired = true;
        }

        pew.Play();
        Ray ray = new Ray(origin.position, origin.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.gameObject.CompareTag("Balloon"))
            {
                Destroy(hit.collider.gameObject);
                RecordBalloonExplosionTime(Time.time);
                pouf.Play();
            }
        }
    }

    private void RecordBalloonExplosionTime(float time)
    {
        balloonExplosionTimes.Add(time);
        endTime = time;
    }

    private void CalculateTimeDifference()
    {
        if (firstShotFired && balloonExplosionTimes.Count > 0)
        {
            float timeDifference = endTime - startTime;

            // update text's content
            GameObject element = GameObject.Find("Temps");
            string stringg = "Temps: " + timeDifference;
            element.GetComponent<TextMeshPro>().text = stringg;

        }
    }
}