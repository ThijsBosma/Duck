using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlayerOnTop : MonoBehaviour
{
    private Transform _playerTransform;

    private MeshFilter visualFilter;

    private PushTree tree;

    private bool playerOnTop;

    // Start is called before the first frame update
    void Start()
    {
        visualFilter = GetComponentInChildren<MeshFilter>();
        tree = GetComponentInParent<PushTree>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && tree._hasInteracted && !playerOnTop)
        {
            playerOnTop = true;
            _playerTransform = collision.transform;

            float stepHeight = visualFilter.mesh.bounds.size.y / 2 + transform.position.y;

            _playerTransform.position += Vector3.up * stepHeight;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ResetOnTop());
        }
    }

    private IEnumerator ResetOnTop()
    {
        yield return new WaitForSeconds(1f);

        playerOnTop = false;
    }
}
