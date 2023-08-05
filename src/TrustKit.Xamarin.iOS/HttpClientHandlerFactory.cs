﻿using System.Net.Http;
using Foundation;
using System.Collections.Generic;
using TrustKit.Xamarin.Core;

namespace TrustKit.Xamarin.iOS
{
    [Preserve(AllMembers = true)]
    public class HttpMessageHandlerFactory : IHttpMessageHandlerFactory
    {
        public const string TSKConfigurationKey = "TSKConfiguration";
        private bool _isInitialized;

        public HttpMessageHandlerFactory() { }

        /// <summary>
        ///     Initialize the Shared Instance of TrustKit with the configuration.
        /// </summary>
        /// <param name="trustKitConfig">The TrustKit configuration. If null is passed, reads in keys from info.plist.</param>
        public void InitSharedInstanceWithConfiguration([NullAllowed] NSDictionary<NSString, NSObject> trustKitConfig = default)
        {
            if (_isInitialized)
            {
                return;
            }

            if (trustKitConfig == null)
            {
                // read them in from info.plist
                NSDictionary trustKitDictionary = (NSDictionary)NSBundle.MainBundle.ObjectForInfoDictionary(TSKConfigurationKey);
                List<NSString> keys = new();
                foreach (NSObject key in trustKitDictionary.Keys)
                {
                    keys.Add((NSString)key);
                }
                NSDictionary<NSString, NSObject> value = new(keys.ToArray(), trustKitDictionary.Values);

                TrustKit.InitSharedInstanceWithConfiguration(value);
            }
            else
            {
                TrustKit.InitSharedInstanceWithConfiguration(trustKitConfig);
            }
            _isInitialized = true;
        }

        /// <inheritdoc />
        public HttpMessageHandler BuildHttpMessageHandler()
        {
            return new TrustKitiOSClientHandler();
        }
    }
}