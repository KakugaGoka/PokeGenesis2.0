using System;
using System.Text.RegularExpressions;
using UnityEngine;

//https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
public static class JsonHelper {
    public static T[] FromJson<T>(string json) {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T> {
        public T[] Items;
    }

    //https://forum.unity.com/threads/json-utility-creates-empty-classes-instead-of-null.471047/
    public static string RemoveEmptyString(string jsonString) {
        string s = Regex.Replace(jsonString, "((\\\".*\\\")+(?=\\:))[:]\\s(\\\"\\\"[,\\r\\n])", string.Empty); //Find vales equal to "" and replace the key and value with empty string
        s = Regex.Replace(s, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline); //Get rid of blank linkes
        s = Regex.Replace(s, @"[,]+(?=\W*\})", string.Empty, RegexOptions.Multiline); //Get rid of any commas that might get left over at the end
        return s;
    }
}
