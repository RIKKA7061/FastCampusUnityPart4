﻿using System;
using UnityEngine;

public class Config
{
	public static readonly string APP_VERSION = Application.version;

	// Preprocessor(JeonCheoRiKi)
#if DEV
	public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.Dev;
#elif STAGE
	public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.Stage;
#elif LIVE
	public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.Live;
#else
	public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.Dev;
#endif

#if DEV
	public const string SERVER_App_API_URL = "https://localhost:7162/";
#elif STAGE
	public const string SERVER_App_API_URL = "https://localhost:7162/";
#elif LIVE
	public const string SERVER_App_API_URL = "https://localhost:7162/";
#else
	public const string SERVER_App_API_URL = "https://localhost:7162/";
#endif


#if UNITY_ANDROID
	public const OS_TYPE E_OS_TYPE = OS_TYPE.Android;
#elif UNITY_IOS
	public const OS_TYPE E_OS_TYPE = OS_TYPE.iOS;
#else
	public const OS_TYPE E_OS_TYPE = OS_TYPE.Android;
#endif
}