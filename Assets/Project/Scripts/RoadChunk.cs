using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RoadChunk : MonoBehaviour 
{
    [SerializeField] Vector3 size;

    public Vector3 GetSize()
    {
        return size;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireCube(this.transform.position + new Vector3(0f, 1f, length / 2f), new Vector3(10f, 0f, length));

        Gizmos.DrawWireCube(this.transform.position + new Vector3(0f, 0f, size.z / 2), size);
    }

    #if UNITY_EDITOR

    [CustomEditor(typeof(RoadChunk))]
    class RoadChunkEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Setup"))
            {
                RoadChunk chunk = (RoadChunk) target;

                MeshFilter mf = chunk.GetComponentInChildren<MeshFilter>();
                Bounds b = mf.sharedMesh.bounds;
                Vector3 localSize = b.size;
                Vector3 worldSize = mf.transform.TransformPoint(localSize);

//                chunk.worldSize = worldSize;

                chunk.size.z = Mathf.Abs(worldSize.x) * 2f;
                chunk.size.y = 0f;
                chunk.size.x = Mathf.Abs(worldSize.y) * 2f;
            }
        }
    }

    #endif
}
