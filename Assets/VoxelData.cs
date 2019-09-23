public class VoxelData
{
    public int MinVisibleValue = 1;
    public int MaxVisibleValue = 1;

    public int Width
    {
        get { return _data.GetLength(1); }
    }

    public int Depth
    {
        get { return _data.GetLength(2); }
    }

    public int Height
    {
        get { return _data.GetLength(0); }
    }

    private byte[,,] _data = new byte[,,] {};


    public VoxelData()
    {

    }

    public VoxelData(byte[,,] data)
    {
        _data = data;
    }

    public byte[,,] GetData() {
        return _data;
    }

    public byte GetCellValue(int x, int y, int z) => _data[y,x,z];

    public bool IsCellVisible(int x, int y, int z)
    {
        var isVisible = false;
        var cellValue = GetCellValue(x,y,z);
        if (cellValue >= MinVisibleValue && cellValue <= MaxVisibleValue ) {
            isVisible = true;
        }
        return isVisible;
    }

    public bool IsCellFaceVisible(CubeFace face, int x, int y, int z)
    {
        var isVisible = false;

        switch(face) {
            case CubeFace.NORT:
                isVisible = z == Depth - 1 || !IsCellVisible(x , y , z + 1);
                break;
            case CubeFace.SOUTH:
                isVisible = z == 0 || !IsCellVisible(x , y , z - 1);
                break;
            case CubeFace.WEST:
                isVisible = x == Depth - 1 || !IsCellVisible(x + 1, y , z);
                break;
            case CubeFace.EAST:
                isVisible = x == 0 || !IsCellVisible(x - 1, y , z);
                break;
            case CubeFace.TOP:
                isVisible = y == Height - 1 || !IsCellVisible(x, y + 1, z);
                break;
            case CubeFace.BOTTOM:
                isVisible = y == 0 || !IsCellVisible(x, y - 1, z);
                break;
        }

        return isVisible;
    }
}
