using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUtils {

    // mouse-position in 2d space when z = 0
    public static Vector3 GetMouseWorldPosition() {
        Vector3 mousePosition = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        mousePosition.z = 0f;
        return mousePosition;
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPostition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPostition);
        return worldPosition;
    }


    // mouse-position in 3d space
    public static Vector3 GetMouseWorldPosition3d(LayerMask mouseColliderLayerMask) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            return raycastHit.point;
        } else {
            return Vector3.zero;
        }
    }


    // draw text-mesh
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000) {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }


    // create text-popup (no parent)
    public static void CreateWorldTextPopup(string text, Vector3 localPosition) {
        CreateWorldTextPopup(null, text, localPosition, 20, Color.white, localPosition + new Vector3(0, 10), 1f);
    }
    
    public static void CreateWorldTextPopup(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, Vector3 finalPopupPosition, float popupTime) {
        TextMesh textMesh = CreateWorldText(parent, text, localPosition, fontSize, color, TextAnchor.LowerLeft, TextAlignment.Left, 5000);
        Transform transform = textMesh.transform;
        Vector3 moveAmount = (finalPopupPosition - localPosition) / popupTime;
        FunctionUpdater.Create(delegate () {
            transform.position += moveAmount * Time.deltaTime;
            popupTime -= Time.deltaTime;
            if (popupTime <= 0f) {
                UnityEngine.Object.Destroy(transform.gameObject);
                return true;
            } else {
                return false;
            }
        }, "WorldTextPopup");
    }

}