#version 120

varying vec2 va_vTex0;
varying vec4 va_vColor;

uniform sampler2D u_s2DTexture;
uniform vec2 u_vResolution;

void main(void) 
{
	float fDistance = 1.0;
  
	for(float fMapY = 0.0; fMapY < u_vResolution.y; fMapY += 1.0) 
	{
		vec2 vNorm = vec2(va_vTex0.s, fMapY / u_vResolution.y) * 2.0 - 1.0;

		float fTheta = 3.141 * 1.5 + vNorm.x * 3.141; 
		float fCoord = (1.0 + vNorm.y) * 0.5;
		
		vec2 vCoord = vec2(-fCoord * sin(fTheta), -fCoord * cos(fTheta)) / 2.0 + 0.5;
		
		vec4 vCoordData = texture2D(u_s2DTexture, vCoord);
		
		float fDist = fMapY / u_vResolution.y;
		
		float fCaster = vCoordData.a;
		if (fCaster > 0.5) 
		{
			fDistance = min(fDistance, fDist);
  		}
	}

	gl_FragData[0] = vec4(vec3(fDistance), 1.0);
}