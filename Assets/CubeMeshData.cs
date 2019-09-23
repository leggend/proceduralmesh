using UnityEngine;

public static class CubeMeshData
{
    private static Vector3[] _vertices = {
        new Vector3(1, 1, 1),
        new Vector3(-1, 1, 1),
        new Vector3(-1, -1, 1),
        new Vector3(1, -1, 1),
        new Vector3(-1, 1, -1),
        new Vector3(1, 1, -1),
        new Vector3(1, -1, -1),
        new Vector3(-1, -1, -1)
    };

    private static int[][] _faceTriangles = {
        new int[] { 0, 1, 2, 3}, // NORTH
        new int[] { 5, 0, 3, 6}, // WEST
        new int[] { 4, 5, 6, 7}, // SOUTH
        new int[] { 1, 4, 7, 2}, // EAST
        new int[] { 5, 4, 1, 0}, // TOP
        new int[] { 3, 2, 7, 6}, // BOTTOM
     };


    public static Vector3[] GetFaceVertices(CubeFace face, float faceScale, Vector3 facePos) {
        Vector3[] faceVertices = new Vector3[4];
         for (var i = 0; i< faceVertices.Length; i++) {
             switch (face) {
                 case CubeFace.NORT:
                    faceVertices[i] = (_vertices[_faceTriangles[0][i]] * faceScale) + facePos;
                    break;
                 case CubeFace.WEST:
                    faceVertices[i] = (_vertices[_faceTriangles[1][i]] * faceScale) + facePos;
                    break;
                 case CubeFace.SOUTH:
                    faceVertices[i] = (_vertices[_faceTriangles[2][i]] * faceScale) + facePos;
                    break;
                 case CubeFace.EAST:
                    faceVertices[i] = (_vertices[_faceTriangles[3][i]] * faceScale) + facePos;
                    break;
                 case CubeFace.TOP:
                    faceVertices[i] = (_vertices[_faceTriangles[4][i]] * faceScale) + facePos;
                    break;
                 case CubeFace.BOTTOM:
                    faceVertices[i] = (_vertices[_faceTriangles[5][i]] * faceScale) + facePos;
                    break;

             }
         }
         return faceVertices;
    }
}

public enum CubeFace {
    NORT = 0,
    WEST = 1,
    SOUTH = 2,
    EAST = 3,
    TOP = 4,
    BOTTOM = 5
}