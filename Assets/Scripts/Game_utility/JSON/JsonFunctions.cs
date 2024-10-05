using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using System;

public class JsonFunctions
{
    // Loads a JSON file and deserializes it into an object of type T.
    public static T CargarJSON<T>(string archivoJson) 
    {
        string contenido = File.ReadAllText(archivoJson); // Reads the JSON file.
        T skin = JsonUtility.FromJson<T>(contenido); // Deserializes the content.
        return skin; // Returns the deserialized object.
    }

    // Loads a specific value from a JSON file using a selector function.
    public static T CargarValorJSON<TypeDataPass, T>(string archivoJson, Func<TypeDataPass, T> selector)
    {
        string contenido = File.ReadAllText(archivoJson); // Reads the JSON file.
        TypeDataPass datos = JsonUtility.FromJson<TypeDataPass>(contenido); // Deserializes the file.
        T valor = selector(datos); // Uses the selector to extract a specific value.
        return valor; // Returns the selected value.
    }

    // Serializes and saves an object to a JSON file.
    public static void GuardarJSON<TypeDataPass>(string archivoJson, TypeDataPass skin)
    {
        string skinJSON = JsonUtility.ToJson(skin); // Serializes the object to JSON.
        File.WriteAllText(archivoJson, skinJSON); // Writes the serialized object to the file.
    }

    // Updates a specific field in a JSON file based on a key (not fully implemented).
    public static void EscribirDatoJSON<TypeDataPass, T>(string archivoJson, string clave, T nuevoValor) 
    {
        TypeDataPass datos = CargarJSON<TypeDataPass>(archivoJson); // Loads the JSON file.

        // Modify the value of the desired field using reflection (incomplete logic).
        PropertyInfo property = datos.GetType().GetProperty(clave);
    }

    // Updates a specific value in a JSON file using selector and setter functions.
    public static void EscribirDatoJSON<TypeDataPass, TValue>(string archivoJson, Func<TypeDataPass, TValue> selector, Action<TypeDataPass, TValue> setter, TValue nuevoValor) where TypeDataPass : class
    {
        TypeDataPass datos = CargarJSON<TypeDataPass>(archivoJson); // Loads the JSON file.

        TValue valorActual = selector(datos); // Retrieves the current value using the selector.
        setter(datos, nuevoValor); // Updates the value using the setter.

        GuardarJSON<TypeDataPass>(archivoJson, datos); // Saves the updated object back to the JSON file.
    }
}
/* toDo: 
        Potential issue: You should check if the file exists before reading or writing to avoid exceptions, especially in CargarJSON.
        Concurrency: If the file is opened or written to multiple times in quick succession, consider handling file locks or using async methods to avoid potential issues.
*/