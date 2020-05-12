// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*=============================================================================
**
** Class: CivicAddress
**
** Purpose: Represents a CivicAddress object
**
=============================================================================*/

using System;
using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Device.Location
{
    public class CivicAddress
    {
        public static readonly CivicAddress Unknown = new CivicAddress();

        //
        // private construcotr for creating single instance of CivicAddress.Unknown
        //
        private CivicAddress()
        {
            AddressLine1  = String.Empty;
            AddressLine2  = String.Empty;
            Building      = String.Empty;
            City          = String.Empty;
            CountryRegion = String.Empty;
            FloorLevel    = String.Empty;
            PostalCode    = String.Empty;
            StateProvince = String.Empty;
        }
            
        public CivicAddress(String addressLine1, String addressLine2, String building, String city, String countryRegion, String floorLevel, String postalCode, String stateProvince)
            : this()
        {
            bool hasField = false;

            if (addressLine1 != null && addressLine1 != String.Empty)
            {
                hasField = true;
                AddressLine1 = addressLine1;
            }
            if (addressLine2 != null && addressLine2 != String.Empty)
            {
                hasField = true;
                AddressLine2 = addressLine2;
            }
            if (building != null && building != String.Empty)
            {
                hasField = true;
                Building = building;
            }
            if (city != null && city != String.Empty)
            {
                hasField = true;
                City = city;
            }
            if (countryRegion != null && countryRegion != String.Empty)
            {
                hasField = true;
                CountryRegion = countryRegion;
            }
            if (floorLevel != null && floorLevel != String.Empty)
            {
                hasField = true;
                FloorLevel = floorLevel;
            }
            if (postalCode != null && postalCode != String.Empty)
            {
                hasField = true;
                PostalCode = postalCode;
            }

            if (stateProvince != null && stateProvince != String.Empty)
            {
                hasField = true;
                StateProvince = stateProvince;
            }

            if (!hasField)
            {
                throw new ArgumentException(SR.GetString(SR.Argument_RequiresAtLeastOneNonEmptyStringParameter));
            }
        }

        public String AddressLine1 { get; private set; }
        public String AddressLine2 { get; private set; }
        public String Building { get; private set; }
        public String City { get; private set; }
        public String CountryRegion { get; private set; }
        public String FloorLevel { get; private set; }
        public String PostalCode { get; private set; }
        public String StateProvince { get; private set; }

        public Boolean IsUnknown {
            get {
                return (String.IsNullOrEmpty(AddressLine1) && String.IsNullOrEmpty(AddressLine2) &&
                        String.IsNullOrEmpty(Building) && String.IsNullOrEmpty(City) && String.IsNullOrEmpty(CountryRegion) && String.IsNullOrEmpty(FloorLevel) && String.IsNullOrEmpty(PostalCode) && String.IsNullOrEmpty(StateProvince));
            }
        }
    }
}
