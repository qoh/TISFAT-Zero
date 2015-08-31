in vec4 pos;
varying vec4 VertCoord;

void main()
{
	gl_Position = gl_ModelViewProjectionMatrix * pos;
	VertCoord = gl_Position;
}