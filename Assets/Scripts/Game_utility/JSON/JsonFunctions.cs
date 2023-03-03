using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using System;
public class JsonFunctions
{
        public static T CargarJSON<T>(string archivoJson) 
        {
                string contenido = File.ReadAllText(archivoJson);
                T skin = JsonUtility.FromJson<T>(contenido);
                return skin;
        }
        public static T CargarValorJSON<TypeDataPass, T>(string archivoJson, Func<TypeDataPass, T> selector)
        {
                string contenido = File.ReadAllText(archivoJson);
                TypeDataPass datos = JsonUtility.FromJson<TypeDataPass>(contenido);
                T valor = selector(datos);
                return valor;
        }
        public static void GuardarJSON<TypeDataPass>(string archivoJson, TypeDataPass skin)
        {
                string skinJSON = JsonUtility.ToJson(skin);
                File.WriteAllText(archivoJson, skinJSON);
        }
        public static void  EscribirDatoJSON<TypeDataPass, T>(string archivoJson, string clave, T nuevoValor) 
        {
                TypeDataPass datos = CargarJSON<TypeDataPass>(archivoJson);

                // Modificar el valor del dato deseado
                PropertyInfo property = datos.GetType().GetProperty(clave);
        }
        public static void EscribirDatoJSON<TypeDataPass, TValue>(string archivoJson, Func<TypeDataPass, TValue> selector, Action<TypeDataPass, TValue> setter, TValue nuevoValor) where TypeDataPass : class
        {
                TypeDataPass datos = CargarJSON<TypeDataPass>(archivoJson);

                TValue valorActual = selector(datos);
                setter(datos, nuevoValor);

                GuardarJSON<TypeDataPass>(archivoJson, datos);


        }//no funciona cargar un solo valor creo que es un tema de tiempo entre abrir un archivo y el otro

}