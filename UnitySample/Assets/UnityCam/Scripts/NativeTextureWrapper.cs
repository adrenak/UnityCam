/*
Copyright (c) 2016 MHD Yamen Saraiji
Copyright (c) 2018 Vatsal Ambastha

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
documentation files (the "Software"), to deal in the Software without restriction, including without 
limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Runtime.InteropServices;

public class NativeTextureWrapper : IDisposable {
	internal const string DllName = "UnityWebcam";

	[DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
	extern static private System.IntPtr CreateTextureWrapper();

	[DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
	extern static private void DeleteTextureWrapper(System.IntPtr w);

	[DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
	extern static private bool SendTexture(System.IntPtr w, System.IntPtr textureID);

	IntPtr m_Instance;

	public NativeTextureWrapper() {
		m_Instance = CreateTextureWrapper();
	}

	public void Send(IntPtr texturePtr) {
		SendTexture(m_Instance, texturePtr);
	}

	public void Dispose() {
		DeleteTextureWrapper(m_Instance);
	}	
}
