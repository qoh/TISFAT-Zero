in vec4 pos;
varying vec4 VertexCoord;

void main()
{
	gl_Position = gl_ModelViewProjectionMatrix * pos;
	VertexCoord = gl_Position;
}