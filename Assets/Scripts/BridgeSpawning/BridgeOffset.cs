using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeOffset : MonoBehaviour
{
    public Vector3 _offsetPosition;
    public Quaternion _offsetRotation;
    public Vector3 _offsetScale;

    public Vector3 size; 

    public BridgeGridSpace _OffsetGridSpace;

    public Collider[] _ObstructionColliders; //Colliders that are in the way of walking over a bridge 

    [Header("Particle")]
    public Vector3 _ParticleOffset;
}
