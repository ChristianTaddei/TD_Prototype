using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRepresentation : MonoBehaviour
{
    private Board board;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Material terrainMaterial;
    private Mesh terrainMesh;

    // public Mesh TerrainMesh { get; set; }

    private Vector3[] vertices;
    public Color[] Colors { get; set; }

    protected void Start()
    {
        // throw new NotImplementedException();
    }

    public void Sync()
    {
        terrainMesh.vertices = vertices;
        meshCollider.sharedMesh = terrainMesh;

        meshCollider.sharedMesh.colors = Colors;

        terrainMesh.RecalculateNormals();
        terrainMesh.RecalculateBounds();
        terrainMesh.RecalculateTangents();
    }

    // public override void SetPrevRepresentedState(BoardState value)
    // {
    //     vertices = value.VertexStates.Select(kvp => kvp.Value.Position).ToArray<Vector3>();
    // }

    // public override void SetNextRepresentedState(BoardState value)
    // {
    //     vertices = value.VertexStates.Select(kvp => kvp.Value.Position).ToArray<Vector3>();
    // }

    public static BoardRepresentation MakeFrom(Board board)
    {


        // Debug.Log("made terrain");
        GameObject representationGameObject = new GameObject("Terrain");

        BoardRepresentation terrainRepresentation = representationGameObject
            .AddComponent<BoardRepresentation>();

        terrainRepresentation.board = board;

        representationGameObject.tag = "Terrain";

        terrainRepresentation.SetupComponents(
            representationGameObject.AddComponent<MeshFilter>(),
            representationGameObject.AddComponent<MeshRenderer>(),
            representationGameObject.AddComponent<MeshCollider>());

        // have to initialize vertices (cant wait sync) to initialize triangles
        terrainRepresentation.vertices = board.Vertices.ToArray<Vector3>();
        terrainRepresentation.terrainMesh.vertices = terrainRepresentation.vertices;
        terrainRepresentation.terrainMesh.triangles = board.Triangles.ToArray<int>();

        // TODO: material from representationManager?
        terrainRepresentation.SetupMaterial(Resources.Load("Materials/GroundMaterial", typeof(Material)) as Material);

        terrainRepresentation.terrainMesh.MarkDynamic();
        terrainRepresentation.Sync();

        // not used
        // terrainRepresentation.meshCollider.sharedMesh = terrainRepresentation.terrainMesh;

        return terrainRepresentation;
    }

    internal bool TryGetSurfacePointFromPosition(int triangleIndex, Vector3 point, out SurfacePoint sp)
    {
        return board.TryGetSurfacePointFromPosition(triangleIndex, point, out sp);
    }

    public void SetupComponents(MeshFilter meshFilter, MeshRenderer meshRenderer, MeshCollider meshCollider)
    {
        this.meshFilter = meshFilter;
        this.meshRenderer = meshRenderer;
        this.meshCollider = meshCollider;
        this.terrainMesh = this.meshFilter.mesh;
        // not used
        //this.meshCollider.sharedMesh = this.terrainMesh;
    }

    // internal bool TryGetBoardPositionFromHit(RaycastHit hit, out BoardPosition boardPosition)
    // {
    //     Face face;
    //     if (SimulationManager.Instance.Board.TryGetFaceFromIndex(hit.triangleIndex, out face))
    //     {
    //         boardPosition = new BoardPosition(SimulationManager.Instance.CurrentState.BoardState, face, hit.point);
    //         return true;
    //     }

    //     boardPosition = default;
    //     return false;
    // }

    public void SetupMaterial(Material material)
    {
        meshRenderer.material = material;
    }

    /*
        public Face getFace(int index)
        {
            // Move to highlight
            Color[] colors = new Color[size * size];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.white;
            }

            colors[terrainMesh.triangles[index * 3 + 0]] = Color.green;
            colors[terrainMesh.triangles[index * 3 + 1]] = Color.green;
            colors[terrainMesh.triangles[index * 3 + 2]] = Color.green;

            terrainMesh.colors = colors;
            return board.Faces[index];
        }
    */
}

// TODO: fix shader or data
/*
        for (int index = 0; index < terrainMesh.triangles.Length; index += 3)
        {
            // Get the three vertices bounding this triangle.
            Vector3 v1 = terrainMesh.vertices[terrainMesh.triangles[index]];
            Vector3 v2 = terrainMesh.vertices[terrainMesh.triangles[index + 1]];
            Vector3 v3 = terrainMesh.vertices[terrainMesh.triangles[index + 2]];

            // Compute a vector perpendicular to the face.
            Vector3 normal = Vector3.Cross(v3 - v1, v2 - v1);

            // Form a rotation that points the z+ axis in this perpendicular direction.
            // Multiplying by the inverse will flatten the triangle into an xy plane.
            Quaternion rotation = Quaternion.Inverse(Quaternion.LookRotation(normal));

            // Assign the uvs, applying a scale factor to control the texture tiling.
            uvs[terrainMesh.triangles[index]] = (Vector2)(rotation * v1);
            uvs[terrainMesh.triangles[index + 1]] = (Vector2)(rotation * v2);
            uvs[terrainMesh.triangles[index + 2]] = (Vector2)(rotation * v3);
        }

        terrainMesh.uv = uvs;
*/
