using UnityEngine;
using UnityEngine.Events;


public class DestroyerTrigger : MonoBehaviour
{
    [Header("Référence brasero et torches")]
    public GameObject braseroVert;
    public GameObject[] torches; // les 3 torches à allumer
    public float delayBetweenTorches = 0.5f;

    [Header("Tag attendu")]
    public string crystalTag = "CrystalVert";
    public UnityEvent onCrystalDestroyed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
