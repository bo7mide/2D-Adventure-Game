using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour {
    public Transform[] Backgrounds;
    public float ParallaxScale, ParallaxReductionFactor,Smoothing;

    private Vector3 _lastPosition;
	// Use this for initialization
	void Start () {
        _lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        var parallax = (_lastPosition.x - transform.position.x) * ParallaxScale;
        for(var i=0;i<Backgrounds.Length;i++)
        {
            var backgroundTargetPosition = Backgrounds[i].position.x + parallax * (i * ParallaxReductionFactor + 1);
            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position,
                new Vector3(backgroundTargetPosition,Backgrounds[i].position.y,Backgrounds[i].position.z),
                Smoothing*Time.deltaTime);
        }
        _lastPosition = transform.position;
	}
}
