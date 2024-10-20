using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

/// <summary>
/// JsonFunctions provides methods to load, save, and update JSON files in a synchronous manner.
/// It includes error handling for file existence and file writing.
/// </summary>
public class JsonFunctions
{
    /// <summary>
    /// Loads a JSON file and deserializes it into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize to.</typeparam>
    /// <param name="archivoJson">The path to the JSON file.</param>
    /// <returns>The deserialized object of type T, or the default value if the file doesn't exist or an error occurs.</returns>
    public static T CargarJSON<T>(string archivoJson)
    {
        if (!File.Exists(archivoJson)) // Check if file exists before reading.
        {
            Debug.LogError($"Error: The file '{archivoJson}' does not exist.");
            return default;
        }

        try
        {
            string contenido = File.ReadAllText(archivoJson); // Read the JSON file.
            T skin = JsonUtility.FromJson<T>(contenido); // Deserialize the content into an object of type T.
            return skin; // Return the deserialized object.
        }
        catch (Exception e)
        {
            Debug.LogError($"Error reading JSON file: {e.Message}");
            return default;
        }
    }

    /// <summary>
    /// Loads a specific value from a JSON file using a selector function.
    /// </summary>
    /// <typeparam name="TypeDataPass">The type of the object to deserialize.</typeparam>
    /// <typeparam name="T">The type of the value to select.</typeparam>
    /// <param name="archivoJson">The path to the JSON file.</param>
    /// <param name="selector">A function to select a specific value from the deserialized object.</param>
    /// <returns>The selected value, or the default value if the file doesn't exist or an error occurs.</returns>
    public static T CargarValorJSON<TypeDataPass, T>(string archivoJson, Func<TypeDataPass, T> selector)
    {
        TypeDataPass datos = CargarJSON<TypeDataPass>(archivoJson); // Load the entire JSON as an object of type TypeDataPass.
        if (datos == null) return default; // Check if data was loaded successfully.
        return selector(datos); // Return the selected value.
    }

    /// <summary>
    /// Serializes and saves an object to a JSON file.
    /// </summary>
    /// <typeparam name="TypeDataPass">The type of object to serialize.</typeparam>
    /// <param name="archivoJson">The path to the JSON file.</param>
    /// <param name="skin">The object to serialize and save.</param>
    public static void GuardarJSON<TypeDataPass>(string archivoJson, TypeDataPass skin)
    {
        try
        {
            string skinJSON = JsonUtility.ToJson(skin, true); // Serialize the object to JSON (formatted for readability).
            File.WriteAllText(archivoJson, skinJSON); // Write the serialized object to the file.
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving JSON file: {e.Message}");
        }
    }

    /// <summary>
    /// Updates a specific value in a JSON file using a selector and setter function.
    /// </summary>
    /// <typeparam name="TypeDataPass">The type of the object to deserialize.</typeparam>
    /// <typeparam name="TValue">The type of the value to update.</typeparam>
    /// <param name="archivoJson">The path to the JSON file.</param>
    /// <param name="selector">A function to select the current value from the deserialized object.</param>
    /// <param name="setter">A function to set the new value in the deserialized object.</param>
    /// <param name="nuevoValor">The new value to set.</param>
    public static void EscribirDatoJSON<TypeDataPass, TValue>(string archivoJson, Func<TypeDataPass, TValue> selector, Action<TypeDataPass, TValue> setter, TValue nuevoValor) where TypeDataPass : class
    {
        TypeDataPass datos = CargarJSON<TypeDataPass>(archivoJson); // Load the JSON data.
        if (datos == null) return; // If data loading fails, exit the function.

        TValue valorActual = selector(datos); // Get the current value using the selector.
        setter(datos, nuevoValor); // Update the value using the setter function.

        GuardarJSON(archivoJson, datos); // Save the updated object back to the JSON file.
    }
}
