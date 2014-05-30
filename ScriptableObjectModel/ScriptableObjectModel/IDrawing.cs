
//------------------------------------------------------------------------------
// <copyright from='2013' to='2014' company='Polar Engineering and Consulting'>
//    Copyright (c) Polar Engineering and Consulting. All Rights Reserved.
//
//    This file contains confidential material.
//
// </copyright>
//------------------------------------------------------------------------------

namespace ScriptableObjectModel
{
    // interface to access non-scriptable portions of the host application
    public interface IDrawing
    {
        int PictureWidth { get; }
        int PictureHeight { get; }
        void DrawLine(int x1, int y1, int x2, int y2);
        void EraseLines();
    }
}
