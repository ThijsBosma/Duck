using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeOffset : MonoBehaviour
{
    public Vector3 _offsetPosition;
    public Quaternion _offsetRotation;
    public Vector3 _offsetScale;

    public BridgeGridSpace _OffsetGridSpace;

    [Header("Particle")]
    public Vector3 _ParticleOffset;
}
