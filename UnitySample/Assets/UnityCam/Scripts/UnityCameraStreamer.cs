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

using UnityEngine;

public class UnityCameraStreamer : MonoBehaviour {
	public bool flip = false;
	public int downsampling = 0;
	public bool blitLocally = true;

	NativeTextureWrapper m_Wrapper;
	Blitter m_Blitter;

	private void Start() {
		m_Wrapper = new NativeTextureWrapper();
		m_Blitter = new Blitter();
	}

	private void OnDestroy() {
		m_Wrapper.Dispose();
		m_Blitter.Dispose();
	}

	public void OnRenderImage(RenderTexture source, RenderTexture destination) {
		Texture src = source;

		// Process the source in the blitter
		int pass = 0;
		if (!flip) pass = 1;
		src = m_Blitter.Process(src, pass, downsampling);

		// Stream the processed output to the wrapper
		src = src.ToTexture2D();
		m_Wrapper.Send(src.GetNativeTexturePtr());

		// Blit locally if yes to show the output on the game screen
		if (blitLocally)
			Graphics.Blit(source, destination);
	}
}
