#version 120

varying vec2 va_vTex0;
varying vec4 va_vColor;

void main()
{
	va_vColor = gl_Color;
	va_vTex0 = gl_MultiTexCoord0;
	gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
}