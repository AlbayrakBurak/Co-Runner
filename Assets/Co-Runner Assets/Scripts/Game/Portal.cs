using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum SpawnerState
    {
        additive,
        multiplier
    }

    public SpawnerState currentMathState;
    private FollowerCreator followerCreator;
    private TextMesh sizeText;
    private MeshRenderer meshRenderer;
    public int size;
    public static bool isGateActive = true;
    private ParticleSystem particlePlayer;


    private void Awake()
    {
        followerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<FollowerCreator>();
        sizeText = GetComponentInChildren<TextMesh>();
        meshRenderer = GetComponent<MeshRenderer>();
      
    }

    private void Start()
    {
        size=Random.Range(2,4);
        switch (currentMathState)
        {
            case SpawnerState.additive:
                sizeText.text = "+" + size.ToString();
                break;
            case SpawnerState.multiplier:
                sizeText.text = "x" + size.ToString();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGateActive)
        {
            meshRenderer.enabled = false;
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.GetComponent<AudioSource>().Play();

            StartCoroutine(GateActive());

            switch (currentMathState)
            {
                case SpawnerState.additive:
                    followerCreator.SpawnPlayer(size);
                    

                    break;
                case SpawnerState.multiplier:
                    int multiplierSize = followerCreator.players.Count * size - followerCreator.players.Count;

                    followerCreator.SpawnPlayer(multiplierSize);
                    break;
            }
        }
    }

    public IEnumerator GateActive()
    {
        isGateActive = false;
        yield return new WaitForSeconds(1.1f);
        isGateActive = true;
    }
}
