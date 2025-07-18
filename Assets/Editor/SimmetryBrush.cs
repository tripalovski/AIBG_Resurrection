using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;

/// <summary>
/// SymmetryBrush creates indentical ordering of tiles no matter how you turn (in 90° step) TIlemap
/// This is to make sure no matter which corner Player gets, he will be on same distance to anything as others
/// </summary>


//[CreateAssetMenu(menuName = "Custom Brushes/Symmetry Brush")]
[CustomGridBrush(true, false, false, "Symmetry Brush")]
public class SymmetryBrush : GridBrush
{
    public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position) {
        int distanceFromXAxis = position.x;
        int distanceFromYAxis = position.y;


        base.Paint(grid, brushTarget, position);
        // Others are rotated for 90° so it can be fair map

        // Explanation only for first one: when Tilemap is rotated for 90° counterclockwise X axis comes in place of Y axis and if distance
        // from X axis was x  it will be mapped on Y axis as x and if from Y axis was y it will be mapped on X axis as -y
        base.Paint(grid, brushTarget, new Vector3Int(-distanceFromYAxis, distanceFromXAxis, position.z));
        base.Paint(grid, brushTarget, new Vector3Int(-distanceFromXAxis, -distanceFromYAxis, position.z));
        base.Paint(grid, brushTarget, new Vector3Int(distanceFromYAxis, -distanceFromXAxis, position.z));
    }

    public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position) {
        int distanceFromXAxis = position.x;
        int distanceFromYAxis = position.y;


        base.Erase(grid, brushTarget, position);
        base.Erase(grid, brushTarget, new Vector3Int(-distanceFromYAxis, distanceFromXAxis, position.z));
        base.Erase(grid, brushTarget, new Vector3Int(-distanceFromXAxis, -distanceFromYAxis, position.z));
        base.Erase(grid, brushTarget, new Vector3Int(distanceFromYAxis, -distanceFromXAxis, position.z));
    }
}


    [CustomEditor(typeof(SymmetryBrush))]
public class SymmetryBrushEditor : GridBrushEditor
{
    private SymmetryBrush lineBrush { get { return target as SymmetryBrush; } }
    public override void OnPaintSceneGUI(GridLayout grid, GameObject brushTarget, BoundsInt position, GridBrushBase.Tool tool, bool executing) {
        int distanceFromXAxis = position.x;
        int distanceFromYAxis = position.y;

        base.OnPaintSceneGUI(grid, brushTarget, new BoundsInt(-distanceFromYAxis, distanceFromXAxis, position.z, 1, 1, 1), tool, executing);
        base.OnPaintSceneGUI(grid, brushTarget, new BoundsInt(-distanceFromXAxis, -distanceFromYAxis, position.z, 1, 1, 1), tool, executing);
        base.OnPaintSceneGUI(grid, brushTarget, new BoundsInt(distanceFromYAxis, -distanceFromXAxis, position.z, 1, 1, 1), tool, executing);
        base.OnPaintSceneGUI(grid, brushTarget, position, tool, executing);
    }
}