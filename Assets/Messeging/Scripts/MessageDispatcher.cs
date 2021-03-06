﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class MessageDispatcher
{
    #region Public Methods
    public static void Send<T>(T message, IEnumerable<GameObject> objects) where T : class
    {
        foreach (var obj in objects)
        {
            InformGameObject(message, obj);
        }
    }
    #endregion PublicMethods

    #region PrivateMethods

    private static void InformGameObject<T>(T message, GameObject obj) where T : class
    {
        var components = obj.GetComponents<Component>();
        foreach (var component in components)
        {
            InformComponent(message, component);
        }
    }

    private static void InformComponent<T>(T message, Component component) where T : class
    {
        var type = component.GetType();
        var methods = type.GetMethods(
            BindingFlags.Instance |         
            BindingFlags.NonPublic |        
            BindingFlags.Public |           
            BindingFlags.FlattenHierarchy | 
            BindingFlags.Static);                                                
        
        foreach (var method in methods)
        {
            var parameters = method.GetParameters();
            
            if (parameters.Length != 1)
            {
                continue;
            }

            var parameterType = parameters[0].ParameterType;
            
            if (parameterType == message.GetType())
            {
                method.Invoke(component, new object[] { message });
            }
        }
    }

    #endregion PrivateMethods
}