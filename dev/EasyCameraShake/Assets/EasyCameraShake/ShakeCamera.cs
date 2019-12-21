using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
	private Transform camPos;  
	public bool useCameraShake = true;      // 手ブレ効果を有効化
	public float angleDampRate = 5.0f;      // 手ブレの減衰比率
	public float noiseSpeed = 0.5f;         // 手ブレ速度
	public float noiseCoeff = 2f;           // 手ブレの大きさ

	void Start() => camPos = transform;
	void Update() => Shake();

	// 手ブレ効果
	private void Shake()
	{
		if (!useCameraShake) return;
		camPos.localRotation = GenerateCameraShakeNoise();
		camPos.localRotation = Quaternion.Slerp(camPos.localRotation, Quaternion.identity, Time.deltaTime * angleDampRate);
	}

	// 手ブレノイズ生成
	private Quaternion GenerateCameraShakeNoise()
	{
		var t = Time.time * noiseSpeed;
		var nx = (Mathf.PerlinNoise(t, t + 5.0f)-0.5f) * noiseCoeff;
		var ny = (Mathf.PerlinNoise(t + 10.0f, t + 15.0f)-0.5f) * noiseCoeff;
		var nz = (Mathf.PerlinNoise(t + 25.0f, t + 20.0f)-0.5f) * noiseCoeff * 0.5f;
		var noise = new Vector3(nx, ny, nz);
		return Quaternion.Euler(noise.x, noise.y, noise.z);
	}
}