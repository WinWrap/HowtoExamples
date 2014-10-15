
//------------------------------------------------------------------------------
// <copyright from='2013' to='2014' company='Polar Engineering and Consulting'>
//    Copyright (c) Polar Engineering and Consulting. All Rights Reserved.
//
//    This file contains confidential material.
//
// </copyright>
//------------------------------------------------------------------------------
using System.Collections.Generic;

namespace ww_ws_objmod
{
    public interface IAppModel
    {
        List<object> AppSortSides(List<object> list);
        List<object> AppSortAngles(List<object> list);
        void AppTrace(string msg);
        ClientImage ClientImage { get; }
    }
}
