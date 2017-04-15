using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Blending : MonoBehaviour {

    SpriteRenderer sr;
    public BlendMode blendMode;
    Color baseColor;

    public enum BlendMode {
        Normal,
        Multiply,
        Darken,
        LinearBurn,
        Lighten,
        ColorDodge,
        Screen,
        Overlay,
        SoftLight,
        HardLight,
        Difference,
        Exclusion,
        Hue,
        Saturation,
        Color,
        Luminosity
    }

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        baseColor = sr.color;
	}
	
	// Update is called once per frame
	void Update () {
        Color c = baseColor; // get base color of sprite

        //for each sprite underneath
        foreach (SpriteRenderer s in GameObject.FindObjectsOfType<SpriteRenderer>()) {
            Transform t = s.gameObject.transform;

            if (t.position.z > transform.position.z) continue;
            // check if object is inside base sprite

            switch (blendMode) {
                case BlendMode.Multiply:
                    c = Multiply(c, s.color); break;
                case BlendMode.Darken:
                    c = Darken(c, s.color); break;
                case BlendMode.LinearBurn:
                    c = LinearBurn(c, s.color); break;
                case BlendMode.Lighten:
                    c = Lighten(c, s.color); break;
                case BlendMode.ColorDodge:
                    c = ColorDodge(c, s.color); break;
                case BlendMode.Screen:
                    c = Screen(c, s.color); break;
                case BlendMode.Overlay:
                    c = Overlay(c, s.color); break;
                case BlendMode.SoftLight:
                    c = SoftLight(c, s.color); break;
                case BlendMode.HardLight:
                    c = HardLight(c, s.color); break;
                case BlendMode.Difference:
                    c = Difference(c, s.color); break;
                case BlendMode.Exclusion:
                    c = Exclusion(c, s.color); break;
                case BlendMode.Hue:
                    c = Hue(c, s.color); break;
                case BlendMode.Saturation:
                    c = Saturation(c, s.color); break;
                case BlendMode.Color:
                    c = Color_(c, s.color); break;
            }
        }
    }

    #region Formulas
    private static Color Multiply(Color a, Color b) {
        Color c = a*b;
        c.a = b.a;
        return c;
    }

    private static Color Darken(Color a, Color b) {
        return new Color(Mathf.Min(a.r, b.r), Mathf.Min(a.g, b.g),
            Mathf.Min(a.b, b.b), b.a);
    }

    private static Color LinearBurn(Color a, Color b) {
        Color c = Vector4.zero;
        c.r = 1 - (1 - a.r) / b.r;
        c.g = 1 - (1 - a.g) / b.g;
        c.b = 1 - (1 - a.b) / b.b;
        c.a = 1 - (1 - a.a) / b.a;
        return c;
    }

    private static Color Lighten(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }

    private static Color ColorDodge(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }

    private static Color Screen(Color a, Color b) {
        Color c = Vector4.zero;
        c.r = 1 - (1 - a.r) * (1 - b.r);
        c.g = 1 - (1 - a.g) * (1 - b.g);
        c.b = 1 - (1 - a.b) * (1 - b.b);
        c.a = 1 - (1 - a.a) * (1 - b.a);
        return c;
    }

    private static Color Overlay (Color a, Color b) {
        Color c = Vector4.zero;
        c.r = a.r < .5f ? 2 * a.r * b.r : 1 - 2 * (a.r) * (1 - b.r);
        c.g = a.g < .5f ? 2 * a.g * b.g : 1 - 2 * (a.g) * (1 - b.g);
        c.b = a.b < .5f ? 2 * a.b * b.b : 1 - 2 * (a.b) * (1 - b.b);
        c.a = a.a < .5f ? 2 * a.a * b.a : 1 - 2 * (a.a) * (1 - b.a);
        return c;
    }

    private static Color SoftLight(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }

    private static Color HardLight(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }

    private static Color Difference(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }

    private static Color Exclusion(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }

    private static Color Hue(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }

    private static Color Saturation(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }

    private static Color Color_(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }

    private static Color Luminosity(Color a, Color b) {
        Color c = Vector4.zero;

        return c;
    }
    #endregion
}
