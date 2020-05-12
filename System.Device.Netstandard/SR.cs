// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*=============================================================================
**
** Class: SR
**
** Purpose: Supplies error messages
**
=============================================================================*/

using static System.Device.Location.SR;

namespace System.Device.Location
{
    internal enum SR
    {
        Argument_MustBeInRangeNegative90to90,
        Argument_MustBeInRangeNegative180To180,
        Argument_MustBeNonNegative,
        Argument_LatitudeOrLongitudeIsNotANumber,
        Argument_MustBeInRangeZeroTo360,
        Argument_RequiresAtLeastOneNonEmptyStringParameter
    }

    internal static class SR2
    {
        private static string arg_range = "argument must be in range ";

        public static string GetString(this SR sr)
        {
            switch (sr)
            {
                case Argument_MustBeInRangeNegative90to90: return arg_range + "[-90,90]";
                case Argument_MustBeInRangeNegative180To180: return arg_range + "[-180,180]";
                case Argument_MustBeNonNegative: return "argument must be non negative";
                case Argument_LatitudeOrLongitudeIsNotANumber: return "latitude or longitude is NaN";
                case Argument_MustBeInRangeZeroTo360: return arg_range + "[-0,360]";
                case Argument_RequiresAtLeastOneNonEmptyStringParameter: return "at least one non empty string parameter is required";
                default: return null;
            }
        }
    }
}
