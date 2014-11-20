
//------------------------------------------------------------------------------
// <copyright from='2013' to='2014' company='Polar Engineering and Consulting'>
//    Copyright (c) Polar Engineering and Consulting. All Rights Reserved.
//
//    This file contains confidential material.
//
// </copyright>
//------------------------------------------------------------------------------
using System.Collections.Generic;

namespace ScriptingModel
{
    public interface IAppModel
    {
        double SideA { get; set; }
        double SideB { get; set; }
        double SideC { get; set; }
        double AngleA { get; set; }
        double AngleB { get; set; }
        double AngleC { get; set; }
        ClientImage TriangleImage { get; set; }
    }
}
