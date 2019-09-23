using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRender : MonoBehaviour
{
    public float Scale = 1f;
    public int MinVisibleValue = 1;
    public int MaxVisibleValue = 1;

    private Mesh _mesh;
    private List<Vector3> _vertices;
    private List<int> _tirangles;
    private VoxelData _data;

    void Awake()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start()
    {
        LoadVoxelData();
        GenerateVoxelMeshData();
        UpdateMesh();
    }

    void LoadVoxelData() {
        byte[,,] data = new byte[,,] {
            {
                {0, 1, 1},
                {1, 1, 1},
                {1, 1, 1}
            },
            {
                {1, 1, 1},
                {1, 1, 0},
                {1, 1, 1}
            },
            {
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1}
            },
            
            {
                {1, 1, 1},
                {1, 0, 1},
                {1, 1, 1}
            },
        };
        _data = new VoxelData(data);
    }

    void GenerateVoxelMeshData() {
        _vertices = new List<Vector3>();
        _tirangles = new List<int>();

        for (int y = 0; y < _data.Height; y++) {
            for (int z = 0; z < _data.Depth; z++) {
                for (int x = 0; x < _data.Width; x++) {
                    if (!_data.IsCellVisible(x , y, z)) {
                        continue;
                    }
                    GenerateCubeMeshData(x, y, z);
                }
            }
        }
    }
    
    void GenerateCubeMeshData(int x, int y, int z) {
        Vector3 cubePos = new Vector3((float) x * Scale, (float) y * Scale, (float) z * Scale);
        float cubeScale = Scale * 0.5f;

        if (_data.IsCellFaceVisible(CubeFace.NORT, x, y, z)) {
            GenerateCubeFaceMeshData(CubeFace.NORT, cubeScale, cubePos);
        }

        if (_data.IsCellFaceVisible(CubeFace.SOUTH, x, y, z)) {
            GenerateCubeFaceMeshData(CubeFace.SOUTH, cubeScale, cubePos);
        }

        if (_data.IsCellFaceVisible(CubeFace.WEST, x, y, z)) {
            GenerateCubeFaceMeshData(CubeFace.WEST, cubeScale, cubePos);
        }

        if (_data.IsCellFaceVisible(CubeFace.EAST, x, y, z)) {
            GenerateCubeFaceMeshData(CubeFace.EAST, cubeScale, cubePos);
        }

        if (_data.IsCellFaceVisible(CubeFace.TOP, x, y, z)) {
            GenerateCubeFaceMeshData(CubeFace.TOP, cubeScale, cubePos);
        }

        if (_data.IsCellFaceVisible(CubeFace.BOTTOM, x, y, z)) {
            GenerateCubeFaceMeshData(CubeFace.BOTTOM, cubeScale, cubePos);
        }
    }

    void GenerateCubeFaceMeshData(CubeFace face, float scale, Vector3 facePos) {
        _vertices.AddRange(CubeMeshData.GetFaceVertices(face, scale, facePos));
        int vCount = _vertices.Count;

        _tirangles.Add(vCount - 4);
        _tirangles.Add(vCount - 4 + 1);
        _tirangles.Add(vCount - 4 + 2);
        _tirangles.Add(vCount - 4);
        _tirangles.Add(vCount - 4 + 2);
        _tirangles.Add(vCount - 4 + 3);

    }
    void UpdateMesh() {
        _mesh.Clear();
        _mesh.vertices = _vertices.ToArray();
        _mesh.triangles = _tirangles.ToArray();
        _mesh.RecalculateNormals();
    }
}
