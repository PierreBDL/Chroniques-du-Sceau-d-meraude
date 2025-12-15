using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Helper pour configurer automatiquement les composants pour une UI responsive
/// Attacher ce script aux panels/groupes d'éléments UI
/// </summary>
public class AutoLayoutHelper : MonoBehaviour
{
    [Header("Layout Settings")]
    [Tooltip("Ajouter automatiquement un Layout Group")]
    public bool addLayoutGroup = true;
    
    [Tooltip("Type de layout à utiliser")]
    public LayoutType layoutType = LayoutType.Vertical;
    
    [Tooltip("Espacement entre les éléments")]
    public float spacing = 10f;
    
    [Tooltip("Padding (gauche, droite, haut, bas)")]
    public RectOffset padding = new RectOffset(10, 10, 10, 10);
    
    [Header("Child Settings")]
    [Tooltip("Forcer l'expansion des enfants en largeur")]
    public bool childForceExpandWidth = true;
    
    [Tooltip("Forcer l'expansion des enfants en hauteur")]
    public bool childForceExpandHeight = false;
    
    [Tooltip("Contrôler la taille des enfants")]
    public bool childControlWidth = true;
    
    [Tooltip("Contrôler la hauteur des enfants")]
    public bool childControlHeight = true;

    public enum LayoutType
    {
        Vertical,
        Horizontal,
        Grid
    }

    void Start()
    {
        if (addLayoutGroup)
        {
            SetupLayoutGroup();
        }
    }

    void SetupLayoutGroup()
    {
        // Supprimer les anciens layout groups
        var oldVertical = GetComponent<VerticalLayoutGroup>();
        var oldHorizontal = GetComponent<HorizontalLayoutGroup>();
        var oldGrid = GetComponent<GridLayoutGroup>();
        
        if (oldVertical != null) DestroyImmediate(oldVertical);
        if (oldHorizontal != null) DestroyImmediate(oldHorizontal);
        if (oldGrid != null) DestroyImmediate(oldGrid);

        switch (layoutType)
        {
            case LayoutType.Vertical:
                SetupVerticalLayout();
                break;
            case LayoutType.Horizontal:
                SetupHorizontalLayout();
                break;
            case LayoutType.Grid:
                SetupGridLayout();
                break;
        }
    }

    void SetupVerticalLayout()
    {
        VerticalLayoutGroup layout = gameObject.AddComponent<VerticalLayoutGroup>();
        layout.spacing = spacing;
        layout.padding = padding;
        layout.childForceExpandWidth = childForceExpandWidth;
        layout.childForceExpandHeight = childForceExpandHeight;
        layout.childControlWidth = childControlWidth;
        layout.childControlHeight = childControlHeight;
        layout.childAlignment = TextAnchor.UpperCenter;
    }

    void SetupHorizontalLayout()
    {
        HorizontalLayoutGroup layout = gameObject.AddComponent<HorizontalLayoutGroup>();
        layout.spacing = spacing;
        layout.padding = padding;
        layout.childForceExpandWidth = childForceExpandWidth;
        layout.childForceExpandHeight = childForceExpandHeight;
        layout.childControlWidth = childControlWidth;
        layout.childControlHeight = childControlHeight;
        layout.childAlignment = TextAnchor.MiddleCenter;
    }

    void SetupGridLayout()
    {
        GridLayoutGroup layout = gameObject.AddComponent<GridLayoutGroup>();
        layout.spacing = new Vector2(spacing, spacing);
        layout.padding = padding;
        layout.cellSize = new Vector2(200, 50);
        layout.childAlignment = TextAnchor.UpperCenter;
    }
}
