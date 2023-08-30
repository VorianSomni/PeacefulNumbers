using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

public class InitializeUGS : MonoBehaviour
{
    public string environment = "production";

    async void Start()
    {
        print("it got here");
        try
        {
            var options = new InitializationOptions().SetEnvironmentName(environment);

            await UnityServices.InitializeAsync(options);
        }
        catch (Exception)
        {
            print("An error occurred during initialization");
        }

    }
}
