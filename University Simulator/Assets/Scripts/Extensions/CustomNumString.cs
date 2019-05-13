using UnityEngine;

public static class CustomNumString {
    public static string ToAbbreviatedString(this int value, int decimals = 1) {
        if (value < 1_000) return value.ToString();
        return ((float) value).ToAbbreviatedString(decimals);
    }

    public static string ToAbbreviatedString(this long value, int decimals = 1) {
        if (value < 1_000) return value.ToString();
        return ((float) value).ToAbbreviatedString(decimals);
    }

    public static string ToAbbreviatedString(this float value, int decimals = 1) {
        string unit = "";
        float denom = 1;
        if (value >= 1_000_000_000_000_000_000) {
            unit = "Qtl";
            denom = 1_000_000_000_000_000_000;
        } else if (value >= 1_000_000_000_000_000) {
            unit = "Qdr";
            denom = 1_000_000_000_000_000;
        } else if (value >= 1_000_000_000_000) {
            unit = "Tr";
            denom = 1_000_000_000_000;
        } else if (value >= 1_000_000_000) {
            unit = "B";
            denom = 1_000_000_000;
        } else if (value >= 1_000_000) {
            unit = "M";
            denom = 1_000_000;
        } else if (value >= 1_000) {
            unit = "K";
            denom = 1_000;
        }
        float precision = Mathf.Pow(10f, decimals);
        return Mathf.Round(value / denom * precision) / precision + unit;
    }
}
