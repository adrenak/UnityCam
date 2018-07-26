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

public static class Extensions {
	public static Texture2D ToTexture2D(this Texture src) {
		if (src == null) return null;

		var t2d = src as Texture2D;
		if(t2d != null)
			return t2d;

		var wct = src as WebCamTexture;
		if (wct != null)
			return wct.ToTexture2D();

		var rt = src as RenderTexture;
		if (rt != null)
			return rt.ToTexture2D();

		return null;
	}

	public static Texture2D ToTexture2D(this WebCamTexture src) {
		var result = new Texture2D(src.width, src.height, TextureFormat.ARGB32, false);
		result.SetPixels(src.GetPixels());
		result.Apply();
		return result;
	}

	public static Texture2D ToTexture2D(this RenderTexture src) {
		RenderTexture.active = src as RenderTexture;
		var result = new Texture2D(src.width, src.height, TextureFormat.ARGB32, false);
		result.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);
		result.Apply();
		RenderTexture.active = null;
		return result;
	}
}
