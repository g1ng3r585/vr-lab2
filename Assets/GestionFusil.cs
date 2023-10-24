using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionFusil : MonoBehaviour
{
    // gun
    private GameObject gunObject;
    private Vector3 initialGunPosition;
    private Quaternion initialGunRotation;

    // rayon
    [SerializeField] private Transform gun;
    [SerializeField] private Transform origin;
    private float distance = 10f;
    private Transform initialGunTransform;

    // sons
    [SerializeField] private AudioSource pew;
    [SerializeField] private AudioSource pouf;

    // gestion du temps
    private bool isPlaying = false;
    private float startTime;
    private float endTime;
    private float bestTime;
    private InputAction action;

    // balloons
    private float balloonCount = 18; 
    private List<GameObject> balloons; 


    // ui
    private TextMeshProUGUI temps;
    private TextMeshProUGUI meilleurTemps;


    public void Start()
    {
        temps = GameObject.Find("Temps").GetComponent<TextMeshProUGUI>();
        meilleurTemps = GameObject.Find("MeilleurTemps").GetComponent<TextMeshProUGUI>();
        bestTime = PlayerPrefs.GetFloat("BestTime", 20000);
        meilleurTemps.text = bestTime == 20000 ? "Meilleur temps: Aucun" : $"Meilleur temps: {bestTime:F3}";
        initialGunTransform = gun;

        // instantiate list of balloons
        balloons = new List<GameObject>(GameObject.FindGameObjectsWithTag("Balloon"));

        gunObject = GameObject.FindWithTag("Gun");
        initialGunPosition = gunObject.transform.position;
        initialGunRotation = gunObject.transform.rotation;
    }


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
            Fire();
        }
    }

    private void OnDisable()
    {
        action.Disable();
        action.performed -= TriggerPerformed;
    }

    private void Fire()
    {
        
        Ray ray = new Ray(origin.position, origin.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.gameObject.CompareTag("Balloon"))
            {
                --balloonCount;
                if (balloonCount == 0) End();
                hit.collider.gameObject.SetActive(false);
                pouf.Play();
            }
        }
    }

    public void PlayPew()
    {
        pew.Play();
    }

    private void StartTimer()
    {
        if (isPlaying == false)
        startTime = Time.time;
        isPlaying = true;
    }

    private void End()
    {
        isPlaying = false;
        endTime =  Time.time - startTime;
        temps.text = $"Temps: {endTime:F3}";
        if (endTime < bestTime)
        {
            meilleurTemps.text = $"Temps: {endTime:F3}";
            PlayerPrefs.SetFloat("BestTime", endTime);
        }
    }

    private void Update()
    {
        if (isPlaying == true && balloonCount > 0)
        {
            temps.text = $"Temps: {(Time.time - startTime):F3}";
        }

    }

    public void Restart()
    {
        if (isPlaying == true) isPlaying = false;
        // reset displayed time to 0
        temps.text = $"Temps: 0,000";
        // reset start time
        startTime = Time.time;
        balloonCount = 18;

        // reactivate balloons
        for (int i = 0; i < balloonCount; i++)
        {
            balloons[i].gameObject.SetActive(true);
        }


        if (gunObject != null)
        {
            gunObject.transform.position = initialGunPosition;
            gunObject.transform.rotation = initialGunRotation;
        }
        else
        {
            Debug.Log("Gun object is null. Make sure it is properly referenced in the scene.");
        }

    }

}