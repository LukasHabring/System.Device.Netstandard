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
using System.Threading;
using System.Diagnostics.CodeAnalysis;

namespace System.Device.Location
{
    public interface ICivicAddressResolver
    {
        CivicAddress ResolveAddress(GeoCoordinate coordinate);
        void ResolveAddressAsync(GeoCoordinate coordinate);
        event EventHandler<ResolveAddressCompletedEventArgs> ResolveAddressCompleted;
    }

    public class ResolveAddressCompletedEventArgs : AsyncCompletedEventArgs
    {
        public ResolveAddressCompletedEventArgs(CivicAddress address, Exception error, Boolean cancelled, Object userState)
                 : base(error, cancelled, userState) 
        {
            Address = address;
        }

        public CivicAddress Address { get; private set;}
    }

    public sealed class CivicAddressResolver : ICivicAddressResolver
    {
        private SynchronizationContext m_synchronizationContext;

        public CivicAddressResolver()
        {
            if (SynchronizationContext.Current == null)
            {
                //
                // Create a SynchronizationContext if there isn't one on calling thread
                //
                m_synchronizationContext = new SynchronizationContext();
            }
            else
            {
                m_synchronizationContext = SynchronizationContext.Current;
            }
        }

        public CivicAddress ResolveAddress(GeoCoordinate coordinate)
        {
            if (coordinate == null)
            {
                throw new ArgumentNullException("coordinate");
            }

            if (coordinate.IsUnknown)
            {
                throw new ArgumentException("coordinate");
            }

            return coordinate.m_address;
        }

        public void ResolveAddressAsync(GeoCoordinate coordinate)
        {
            if (coordinate == null)
            {
                throw new ArgumentNullException("coordinate");
            }

            if (Double.IsNaN(coordinate.Latitude) || Double.IsNaN(coordinate.Longitude))
            {
                throw new ArgumentException("coordinate");
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ResolveAddress), coordinate);
        }

        public event EventHandler<ResolveAddressCompletedEventArgs> ResolveAddressCompleted;

        private void OnResolveAddressCompleted(ResolveAddressCompletedEventArgs e)
        {
            EventHandler<ResolveAddressCompletedEventArgs> t = ResolveAddressCompleted;
            if (t != null) t(this, e);
        }

        /// <summary>Represents a callback to a protected virtual method that raises an event.</summary>
        /// <typeparam name="T">The <see cref="T:System.EventArgs"/> type identifying the type of object that gets raised with the event"/></typeparam>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that should be passed to a protected virtual method that raises the event.</param>
        private delegate void EventRaiser<T>(T e) where T : EventArgs;

        /// <summary>A helper method used by derived types that asynchronously raises an event on the application's desired thread.</summary>
        /// <typeparam name="T">The <see cref="T:System.EventArgs"/> type identifying the type of object that gets raised with the event"/></typeparam>
        /// <param name="callback">The protected virtual method that will raise the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that should be passed to the protected virtual method raising the event.</param>
        private void PostEvent<T>(EventRaiser<T> callback, T e) where T : EventArgs 
        {
            if (m_synchronizationContext != null)
            {
                m_synchronizationContext.Post(delegate(Object state) { callback((T)state); }, e);
            }
        }

        //
        // Thread pool thread used to resolve civic address
        //
        private void ResolveAddress(object state)
        {
            GeoCoordinate coordinate = state as GeoCoordinate;
            if (coordinate != null)
            {
                PostEvent(OnResolveAddressCompleted, new ResolveAddressCompletedEventArgs(coordinate.m_address, null, false, null));
            }
        }
    }
}
