using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class VectorExtensions
{
    public static Vector3 FlipX(this Vector3 v) => new Vector3(-v.x, v.y, v.z);
    public static Vector3 FlipY(this Vector3 v) => new Vector3(v.x, -v.y, v.z);
    public static Vector3 FlipZ(this Vector3 v) => new Vector3(v.x, v.y, -v.z);
}

public class AudioReverbArea : MonoBehaviour {

    // Static so you can control all of them from any component.
    // Set in OnValidate
    public static bool drawGizmos;

    public AudioReverbPreset preset;
    [Range(0.01f,10f)]
    public float fadeDistance = 1f;

    [HideInInspector]
    public AudioReverbData data;
    public int priority = 0;

    public bool drawGizmo;

    protected Collider shape;
    protected BoxCollider boxCollider;
    protected MeshCollider meshCollider;
    protected SphereCollider sphereCollider;
    protected CapsuleCollider capsuleCollider;

    protected virtual Color gizmoColour => new Color(1, 0.75f, 0, 0.7f);

    private void Start() {
        AudioReverbZone zone = gameObject.AddComponent<AudioReverbZone>();
        zone.reverbPreset = preset;
        zone.minDistance = 0f;
        zone.maxDistance = 0f;
        data = new AudioReverbData(zone);
        data.priority = priority;
        data.shape = shape;
        data.fadeDistance = fadeDistance;
        Destroy(zone);
    }

    protected virtual void OnValidate()
    {
        // Collider
        if (!shape)
            shape = GetComponent<Collider>();

        // Mesh
        if (!boxCollider)
            boxCollider = GetComponent<BoxCollider>();

        if (boxCollider)
            boxCollider.isTrigger = true;

        // Sphere
        if (!sphereCollider)
            sphereCollider = GetComponent<SphereCollider>();

        if (sphereCollider)
            sphereCollider.isTrigger = true;

        // Capsule
        if (!capsuleCollider)
            capsuleCollider = GetComponent<CapsuleCollider>();
        
        if (capsuleCollider)
            capsuleCollider.isTrigger = true;

        // Mesh
        if (!meshCollider)
            meshCollider = GetComponent<MeshCollider>();
        
        if (meshCollider)
            meshCollider.isTrigger = true;

        drawGizmos = drawGizmo;
    }

    protected virtual void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        Matrix4x4 originalMatrix = Gizmos.matrix;

        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = gizmoColour;

        if (boxCollider)
        {
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);

            Gizmos.color *= new Color(1, 1, 1, 0.5f);
            Gizmos.DrawCube(boxCollider.center, boxCollider.size);

            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size + (Vector3.one * fadeDistance));

            Gizmos.DrawLine(boxCollider.center + boxCollider.size / 2, boxCollider.center + (boxCollider.size / 2) + (Vector3.one * fadeDistance / 2));
            Gizmos.DrawLine(boxCollider.center - boxCollider.size / 2, boxCollider.center - (boxCollider.size / 2) - (Vector3.one * fadeDistance / 2));
            Gizmos.DrawLine(boxCollider.center + boxCollider.size.FlipX() / 2, boxCollider.center + (boxCollider.size.FlipX() / 2) + (Vector3.one.FlipX() * fadeDistance / 2));
            Gizmos.DrawLine(boxCollider.center - boxCollider.size.FlipX() / 2, boxCollider.center - (boxCollider.size.FlipX() / 2) - (Vector3.one.FlipX() * fadeDistance / 2));
            Gizmos.DrawLine(boxCollider.center - boxCollider.size.FlipY() / 2, boxCollider.center - (boxCollider.size.FlipY() / 2) - (Vector3.one.FlipY() * fadeDistance / 2));
            Gizmos.DrawLine(boxCollider.center + boxCollider.size.FlipY() / 2, boxCollider.center + (boxCollider.size.FlipY() / 2) + (Vector3.one.FlipY() * fadeDistance / 2));
            Gizmos.DrawLine(boxCollider.center + boxCollider.size.FlipZ() / 2, boxCollider.center + (boxCollider.size.FlipZ() / 2) + (Vector3.one.FlipZ() * fadeDistance / 2));
            Gizmos.DrawLine(boxCollider.center - boxCollider.size.FlipZ() / 2, boxCollider.center - (boxCollider.size.FlipZ() / 2) - (Vector3.one.FlipZ() * fadeDistance / 2));
        }

        if (sphereCollider)
        {
            Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius);

            Gizmos.color *= new Color(1, 1, 1, 0.5f);
            Gizmos.DrawSphere(sphereCollider.center, sphereCollider.radius);

            Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius + fadeDistance);

            Gizmos.DrawLine(sphereCollider.center + Vector3.left * sphereCollider.radius, Vector3.left * (sphereCollider.radius + fadeDistance));
            Gizmos.DrawLine(sphereCollider.center + Vector3.right * sphereCollider.radius, Vector3.right * (sphereCollider.radius + fadeDistance));
            Gizmos.DrawLine(sphereCollider.center + Vector3.up * sphereCollider.radius, Vector3.up * (sphereCollider.radius + fadeDistance));
            Gizmos.DrawLine(sphereCollider.center + Vector3.down * sphereCollider.radius, Vector3.down * (sphereCollider.radius + fadeDistance));
            Gizmos.DrawLine(sphereCollider.center + Vector3.forward * sphereCollider.radius, Vector3.forward * (sphereCollider.radius + fadeDistance));
            Gizmos.DrawLine(sphereCollider.center + Vector3.back * sphereCollider.radius, Vector3.back * (sphereCollider.radius + fadeDistance));
        }

        if (capsuleCollider)
        {
            Gizmos.DrawWireSphere(capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius), capsuleCollider.radius);
            Gizmos.DrawWireSphere(capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius), capsuleCollider.radius);

            Gizmos.DrawLine(capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius) + Vector3.forward * capsuleCollider.radius,
                capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius) + Vector3.forward * capsuleCollider.radius);
            Gizmos.DrawLine(capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius) + Vector3.back * capsuleCollider.radius,
                capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius) + Vector3.back * capsuleCollider.radius);
            Gizmos.DrawLine(capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius) + Vector3.right * capsuleCollider.radius,
                capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius) + Vector3.right * capsuleCollider.radius);
            Gizmos.DrawLine(capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius) + Vector3.left * capsuleCollider.radius,
                capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius) + Vector3.left * capsuleCollider.radius);

            Gizmos.color *= new Color(1, 1, 1, 0.5f);
            Gizmos.DrawSphere(capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius), capsuleCollider.radius);
            Gizmos.DrawSphere(capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius), capsuleCollider.radius);

            Gizmos.DrawWireSphere(capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius), capsuleCollider.radius + fadeDistance);
            Gizmos.DrawWireSphere(capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius), capsuleCollider.radius + fadeDistance);
        }

        if (meshCollider)
        {
            Gizmos.DrawWireMesh(meshCollider.sharedMesh);

            Gizmos.color *= new Color(1, 1, 1, 0.5f);
            Gizmos.DrawMesh(meshCollider.sharedMesh);
        }

        Gizmos.matrix = originalMatrix;
    }
}
