#version 120

varying vec2 va_vTex0;
varying vec4 va_vColor;

uniform sampler2D u_s2DTexture;
uniform vec2 u_vResolution;

void main(void) 
{
	vec2 vNorm = vec2(va_vTex0.s, 1.0 - va_vTex0.t) * 2.0 - 1.0;

	float fTheta = atan(vNorm.y, vNorm.x);
	float fLength = length(vNorm);	
	float fCoord = (fTheta + 3.141) / (2.0 * 3.141);

	vec2 vShadowCoord = vec2(fCoord, 0.0);
	
	float fCenter = step(fLength, texture2D(u_s2DTexture, vec2(vShadowCoord.x, vShadowCoord.y)).r);
	float fBlur = (1./u_vResolution.x)  * smoothstep(0., 1., fLength); 
	float fSum = 0.0;
	
	fSum += step(fLength, texture2D(u_s2DTexture, vec2(vShadowCoord.x - 4.0 * fBlur, vShadowCoord.y)).r) * 0.05;
	fSum += step(fLength, texture2D(u_s2DTexture, vec2(vShadowCoord.x - 3.0 * fBlur, vShadowCoord.y)).r) * 0.09;
	fSum += step(fLength, texture2D(u_s2DTexture, vec2(vShadowCoord.x - 2.0 * fBlur, vShadowCoord.y)).r) * 0.12;
	fSum += step(fLength, texture2D(u_s2DTexture, vec2(vShadowCoord.x - 1.0 * fBlur, vShadowCoord.y)).r) * 0.15;

	fSum += fCenter * 0.16;

	fSum += step(fLength, texture2D(u_s2DTexture, vec2(vShadowCoord.x + 1.0 * fBlur, vShadowCoord.y)).r) * 0.15;
	fSum += step(fLength, texture2D(u_s2DTexture, vec2(vShadowCoord.x + 2.0 * fBlur, vShadowCoord.y)).r) * 0.12;
	fSum += step(fLength, texture2D(u_s2DTexture, vec2(vShadowCoord.x + 3.0 * fBlur, vShadowCoord.y)).r) * 0.09;
	fSum += step(fLength, texture2D(u_s2DTexture, vec2(vShadowCoord.x + 4.0 * fBlur, vShadowCoord.y)).r) * 0.05;
	
 	float fLit = mix(fCenter, fSum, 1.0);
 	
 	gl_FragData[0] = va_vColor * vec4(vec3(1.0), fLit * smoothstep(1.0, 0.0, fLength));
}