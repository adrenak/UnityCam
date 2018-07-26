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
using UnityEngine;

public class Blitter : IDisposable {
	public const string k_ShaderName = "UnityCam/Image/Blitter";
	Material m_ProcessingMaterial;
	RenderTextureFormat m_RTFormat;
	RenderTexture m_ProcessedTexture;

	public Blitter() {
		m_RTFormat = RenderTextureFormat.Default;
		m_ProcessingMaterial = new Material(Shader.Find(k_ShaderName));
	}

	public void Dispose() {
		MonoBehaviour.Destroy(m_ProcessingMaterial);
		MonoBehaviour.Destroy(m_ProcessedTexture);
	}

	public Texture Process(Texture _inputTex, int _pass, int _downsampling = 0) {
		if (_inputTex == null || _inputTex.width == 0 || _inputTex.height == 0) return _inputTex;

		CreateProcessedTexture(_inputTex, _downsampling);
		m_ProcessingMaterial.mainTexture = _inputTex;
		Graphics.Blit(_inputTex, m_ProcessedTexture, m_ProcessingMaterial, _pass);
		return m_ProcessedTexture;
	}

	void CreateProcessedTexture(Texture _inputTex, int _downsampling) {
		int width = _inputTex.width / (_downsampling + 1);
		int height = _inputTex.height / (_downsampling + 1);

		if ((_inputTex as Texture2D != null) && ((Texture2D)_inputTex).format == TextureFormat.Alpha8)
			height = (int)(height / 1.5f);

		if (m_ProcessedTexture == null || m_ProcessedTexture.width != width || m_ProcessedTexture.height != height)
			m_ProcessedTexture = new RenderTexture(width, height, 15, m_RTFormat);
	}
}
