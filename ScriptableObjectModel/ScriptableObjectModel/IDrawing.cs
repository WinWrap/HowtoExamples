
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
    // used by application, not used by WinWrap Basic scripts
    public interface IDrawing
    {
        int PictureWidth { get; }
        int PictureHeight { get; }
        void DrawLine(int x1, int y1, int x2, int y2);
        void EraseLines();
    }
}
