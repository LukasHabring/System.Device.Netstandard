// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*=============================================================================
**
** Class: Internal types/classes used by Location API
**
** Purpose: Represents helpers and Location API COM interops used by the API
**
=============================================================================*/

using System;
using System.Security;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Device.Location.Internal
{
    #region Helpers
    internal static class Utility
    {
        [Conditional("DEBUG")]
        public static void DebugAssert(bool condition, string message)
        {
            System.Diagnostics.Debug.Assert(condition, message);
        }

        [Conditional("DEBUG")]
        public static void Trace(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }
    }
    #endregion
}
