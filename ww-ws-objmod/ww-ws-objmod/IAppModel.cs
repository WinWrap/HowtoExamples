﻿
//------------------------------------------------------------------------------
// <copyright from='2013' to='2014' company='Polar Engineering and Consulting'>
//    Copyright (c) Polar Engineering and Consulting. All Rights Reserved.
//
//    This file contains confidential material.
//
// </copyright>
//------------------------------------------------------------------------------

namespace ww_ws_objmod
{
    public interface IAppModel
    {
        int PictureWidth { get; }
        void EraseLines();
        //void ErrorAppend(string serror);
        AClass TheAClass { get; }
    }
}
