using System.Object;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class OdinTest : GlobalConfig<ColorPaletteManager>, IGlobalConfigEvents
{
    [ColorPalette]
    public Color ColorOptions;

    [ColorPalette("Underwater")]
    public Color UnderwaterColor;

    [ColorPalette("My Palette")]
    public Color MyColor;

    public string DynamicPaletteName = "Clovers";

    // The ColorPalette attribute supports both
    // member references and attribute expressions.
    [ColorPalette("$DynamicPaletteName")]
    public Color DynamicPaletteColor;

    [ColorPalette("Fall"), HideLabel]
    public Color WideColorPalette;

    [ColorPalette("Clovers")]
    public Color[] ColorArray;

    // ------------------------------------
    // Color palettes can be accessed and modified from code.
    // Note that the color palettes will NOT automatically be included in your builds.
    // But you can easily fetch all color palettes via the ColorPaletteManager
    // and include them in your game like so:
    // ------------------------------------

    [FoldoutGroup("Color Palettes", expanded: false)]
    [ListDrawerSettings(IsReadOnly = true)]
    [PropertyOrder(9)]
    public List<ColorPalette> ColorPalettes;

    [Serializable]
    public class ColorPalette
    {
        [HideInInspector]
        public string Name;

        [LabelText("$Name")]
        [ListDrawerSettings(IsReadOnly = true, ShowFoldout = false)]
        public Color[] Colors;
    }

    [FoldoutGroup("Color Palettes"), Button(ButtonSizes.Large), GUIColor(0, 1, 0), PropertyOrder(8)]
    private void FetchColorPalettes()
    {
        this.ColorPalettes = Sirenix
            .OdinInspector.Editor.ColorPaletteManager.Instance.ColorPalettes.Select(
                x => new ColorPalette() { Name = x.Name, Colors = x.Colors.ToArray() }
            )
            .ToList();
    }
}
