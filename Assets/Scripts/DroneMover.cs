using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine.UI;

public class DroneMover : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    public Vector2d railMuseum;
    public Vector2d temple;
    public Vector2d marblePalace;

    bool shouldMove = false;
    bool isArrived = false;
    public float droneSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _map = FindObjectOfType<AbstractMap>();
        shouldMove = true;

        Button buttonRailMuseum = GameObject.Find("RailMuseumButton").GetComponent<Button>();
        buttonRailMuseum.onClick.AddListener(() => { FlyToRailMuseum(); });

        Button butttonTemple = GameObject.Find("TempleButton").GetComponent<Button>();
        butttonTemple.onClick.AddListener(() => { FlyToTemple(); });

        Button buttonMarblePalace = GameObject.Find("MarblePalaceButton").GetComponent<Button>();
        buttonMarblePalace.onClick.AddListener(() => { FlyToMarblePalace(); }); 
    }

    public void FlyToRailMuseum()
    {
        StopAllCoroutines();
        StartCoroutine(FlyToPoint(railMuseum));
    }

    public void FlyToTemple()
    {
        StopAllCoroutines();
        StartCoroutine(FlyToPoint(temple));
    }

    public void FlyToMarblePalace()
    {
        StopAllCoroutines();
        StartCoroutine(FlyToPoint(marblePalace));
    }

    IEnumerator FlyToPoint(Vector2d locationPoint)
    {
        Vector3 destination = _map.GeoToWorldPosition(locationPoint);

        while(Vector3.Distance(transform.position, destination) > 0.01f)
        {
            var distanceCovered = droneSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, destination, distanceCovered);
            yield return 0;
        }

    }
}
