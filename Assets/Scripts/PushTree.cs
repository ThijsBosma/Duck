using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTree : MonoBehaviour, IInteractable
{
    [SerializeField] public Transform _BridgePosition;
    [SerializeField] public GameObject _BridgToSpawn;

    [SerializeField] private float _MoveTime;

    [SerializeField] private AnimationCurve _Curve;

    private AnimationEvent animationEvent; 
    private Animator animator;
    public AnimationClip animationClip;

    private GetSeedBack _getSeed;

    private float _time;

    private bool _hasInteracted;

    private void Start()
    {
        animator = GetComponent<Animator>();

        _getSeed = GetComponent<GetSeedBack>();

        this.enabled = false;
    }

    public void Interact()
    {
        animator.Play("FallOver");
        _hasInteracted = true;
    }

    /// <summary>
    /// Called by animation even
    /// </summary>
    public void SpawnBridge()
    {
        Instantiate(_BridgToSpawn, _BridgePosition.position, _BridgePosition.rotation);
        Destroy(gameObject);
    }

    public void UnInteract()
    {
    }

    public bool HasInteracted()
    {
        InteractText.instance.ResetText();
        return _hasInteracted;
    }

    public void EnablePushTree()
    {
        this.enabled = true;
        _getSeed.enabled = false;
    }
}
